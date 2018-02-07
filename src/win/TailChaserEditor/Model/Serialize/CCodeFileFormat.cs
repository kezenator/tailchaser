using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.Model.Serialize
{
    public class CCodeFileFormat
    {
        public static string Serialize(Scheme scheme)
        {
            TextSerializer s = new TextSerializer();

            BinarySerializer bs = InternalSerializeBinary(scheme);

            s.AddMultilineDeoxygenComment(new string[] {
                "@file",
                "",
                "TailChaser scheme \"" + scheme.Name + "\"",
                "",
                "Format: TailChaser Scheme, version 2018-02-03" });
            s.AddLine("#ifndef __SCHEME__" + scheme.Name + "_H__");
            s.AddLine("#define __SCHEME__" + scheme.Name + "_H__");
            s.AddLine("#include <avr/pgmspace.h>");
            s.AddLine("#include <stdint.h>");
            s.AddLine("#include <stddef.h>");
            s.AddLine("const size_t " + scheme.Name + "_size = " + bs.TotalBytes + ";");
            s.AddLine("const uint8_t " + scheme.Name + "[" + bs.TotalBytes + "] PROGMEM =");
            s.AddLine("{");
            s.AddBinary(bs.Lines);

            s.AddLine("};");
            s.AddLine("#endif // __SCHEME__" + scheme.Name + "_H__");

            return s.ToString();
        }

        public static byte[] SerializeBinary(Scheme scheme)
        {
            return InternalSerializeBinary(scheme).Bytes;
        }

        private static BinarySerializer InternalSerializeBinary(Scheme scheme)
        {
            BinarySerializer bs = new BinarySerializer();

            bs.WriteLengthAndUtf8String(scheme.Name, "Invalid scheme name");
            bs.CommitLineFormat("Name: \"{0}\"", scheme.Name);
            bs.WriteLengthAndUtf8String(scheme.Description, "Invalid scheme description");
            bs.CommitLineFormat("Description: \"{0}\"", scheme.Description);

            bs.WriteIntAsUint8(scheme.NumLayers, "Scheme has too many layers");
            bs.CommitLineFormat("{0} Layer(s)", scheme.NumLayers);

            foreach (Layer l in scheme.Layers)
            {
                bs.WriteLengthAndUtf8String(l.Name, "Invalid layer name");
                bs.CommitLineFormat("Layer \"{0}\"", l.Name);

                bs.WriteBytes(new byte[] { l.SignalSet.GetMaskAsByte(), l.SignalSet.GetValueAsByte() });
                bs.CommitLine(l.SignalSet.ToString());

                WriteBitmap(l.Bitmap, bs);
            }

            return bs;
        }

        public static Scheme Deserialize(string contents, Palette palette)
        {
            TextDeserializer tds = new TextDeserializer(contents);

            tds.ExpectLine("/**");
            tds.ExpectLine(" * @file");
            tds.ExpectLine(" *");
            string name = tds.DecodeExpectedLine(" * TailChaser scheme \"", "\"");
            tds.ExpectLine(" *");
            tds.ExpectLine(" * Format: TailChaser Scheme, version 2018-02-03");
            tds.ExpectLine(" */");
            tds.ExpectLine("#ifndef __SCHEME__" + name + "_H__");
            tds.ExpectLine("#define __SCHEME__" + name + "_H__");
            tds.ExpectLine("#include <avr/pgmspace.h>");
            tds.ExpectLine("#include <stdint.h>");
            tds.ExpectLine("#include <stddef.h>");
            string size_str = tds.DecodeExpectedLine("const size_t " + name + "_size = ", ";");
            int size = int.Parse(size_str);
            tds.ExpectLine("const uint8_t " + name + "[" + size_str + "] PROGMEM =");
            tds.ExpectLine("{");
            byte[] bytes = tds.ExpectBytesUntil("};");
            tds.ExpectLine("};");
            tds.ExpectLine("#endif // __SCHEME__" + name + "_H__");
            tds.ExpectEof();

            if (bytes.Length != size)
            {
                throw new FormatException("Raw byte length doesn't match specified size");
            }

            Scheme scheme = new Scheme(palette);

            BinaryDeserializer bds = new BinaryDeserializer(bytes);

            string encoded_name = bds.ReadString();

            if (!name.Equals(encoded_name))
                throw new FormatException("Encoded name doesn't match commented name");

            scheme.Name = encoded_name;
            scheme.Description = bds.ReadString();

            int num_layers = bds.ReadUint8();

            for (int i = 0; i < num_layers; ++i)
            {
                Layer layer = scheme.AddLayer();

                layer.Name = bds.ReadString();
                layer.SignalSet = SignalSet.FromMaskAndValue(bds.ReadUint8(), bds.ReadUint8());
                ReadBitmap(layer.Bitmap, bds.ReadBytes(320));
            }

            bds.CheckNoBytesRemaining();

            return scheme;
        }

        private static void WriteBitmap(Bitmap bitmap, BinarySerializer bs)
        {
            for (int y = 0; y < bitmap.Height; ++y)
            {
                StringBuilder sb = new StringBuilder();
                BitStuffer bitstuffer = new BitStuffer();

                int num_bits = bitmap.Palette.NumBits;

                for (int x = 0; x < bitmap.Width; ++x)
                {
                    int index = bitmap[x, y];

                    bitstuffer.AddBits((byte)index, num_bits);

                    if (bitmap.Palette.IsTransparent(index))
                        sb.Append('.');
                    else
                    {
                        System.Drawing.Color c = bitmap.Palette[index];

                        float hue = c.GetHue();
                        float lit = c.GetBrightness();
                        float sat = c.GetSaturation();

                        if (lit > 0.9)
                            sb.Append('#'); // White
                        else if (lit < 0.1)
                            sb.Append('-'); // Black
                        else if (sat < 0.2)
                            sb.Append('~'); // Grey
                        else if (hue < 20)
                            sb.Append('R'); // Red
                        else if (hue < 40)
                            sb.Append('O'); // Orange
                        else if (hue < 90)
                            sb.Append('Y'); // Yellow
                        else if (hue < 150)
                            sb.Append('G'); // Green
                        else if (hue < 210)
                            sb.Append('C'); // Cyan
                        else if (hue < 270)
                            sb.Append('B'); // Blue
                        else if (hue < 330)
                            sb.Append('M'); // Magenta
                        else
                            sb.Append('R'); // Red (remainder 30 deg around 0
                    }
                }

                bs.WriteBytes(bitstuffer.Bytes);
                bs.CommitLine(sb.ToString());
            }
        }

        private static void ReadBitmap(Bitmap bitmap, byte[] bytes)
        {
            Debug.Assert(bitmap.Palette.NumBits == 5);
            Debug.Assert((bitmap.Width % 8) == 0);
            Debug.Assert(bytes.Length == 320);

            int index = 0;

            for (int row = 0; row < bitmap.Height; ++row)
            {
                for (int column = 0; column < bitmap.Width; column += 8)
                {
                    byte b1 = bytes[index + 0];
                    byte b2 = bytes[index + 1];
                    byte b3 = bytes[index + 2];
                    byte b4 = bytes[index + 3];
                    byte b5 = bytes[index + 4];

                    bitmap[column + 0, row] = b1 >> 3;
                    bitmap[column + 1, row] = ((b1 & 0x07) << 2) | (b2 >> 6);
                    bitmap[column + 2, row] = (b2 >> 1) & 0x1F;
                    bitmap[column + 3, row] = ((b2 << 4) & 0x10) | ((b3 >> 4) & 0x0F);
                    bitmap[column + 4, row] = ((b3 << 1) & 0x1E) | ((b4 >> 7) & 0x01);
                    bitmap[column + 5, row] = (b4 >> 2) & 0x1F;
                    bitmap[column + 6, row] = ((b4 << 3) & 0x18) | ((b5 >> 5) & 0x07);
                    bitmap[column + 7, row] = b5 & 0x1F;

                    index += 5;
                }
            }
        }
    }
}
