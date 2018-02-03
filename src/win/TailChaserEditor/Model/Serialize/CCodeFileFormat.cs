using System;
using System.Collections.Generic;
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

            BinarySerializer bs = new BinarySerializer();

            {
                bs.WriteLengthAndUtf8String(scheme.Name, "Invalid scheme name");
                bs.CommitLineFormat("Name: \"{0}\"", scheme.Name);
                bs.WriteLengthAndUtf8String(scheme.Description, "Invalid scheme description");
                bs.CommitLineFormat("Description: \"{0}\"", scheme.Description);

                bs.WriteIntAsUint8(scheme.NumLayers, "Scheme has too many layers");
                bs.CommitLineFormat("{0} Layers", scheme.NumLayers);

                foreach (Layer l in scheme.Layers)
                {
                    bs.WriteLengthAndUtf8String(l.Name, "Invalid layer name");
                    bs.CommitLineFormat("Layer \"{0}\"", l.Name);

                    bs.WriteBytes(new byte[] { l.SignalSet.GetMaskAsByte(), l.SignalSet.GetValueAsByte() });
                    bs.CommitLine(l.SignalSet.ToString());

                    WriteBitmap(l.Bitmap, bs);
                }
            }

            s.AddMultilineComment(new string[] { "TailChaser Scheme, format 2018-02-03"});
            s.AddLine("#ifndef __SCHEME__" + scheme.Name + "_H__");
            s.AddLine("#define __SCHEME__" + scheme.Name + "_H__");
            s.AddLine("#include <avr/pgmspace.h>");
            s.AddLine("#include <stdint.h>");
            s.AddLine("#include <stddef.h>");
            s.AddLine("const size_t " + scheme.Name + "_size = " + bs.TotalBytes + ";");
            s.AddLine("const unsigned char " + scheme.Name + "[" + bs.TotalBytes + "] PROGMEM =");
            s.AddLine("{");
            s.AddBinary(bs.Lines);

            s.AddLine("};");
            s.AddLine("#endif // __SCHEME__" + scheme.Name + "_H__");

            return s.ToString();
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
                        else if (hue < 30)
                            sb.Append('R'); // Red
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
    }
}
