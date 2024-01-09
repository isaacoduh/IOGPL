using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IOGPL
{
    public class IconCommand
    {
        Proto proto;
        public IconCommand(Proto proto) {
        this.proto = proto;
        }

        public void Handle(string[] parts, Dictionary<string, int> variables, Proto proto, ref int variableCounter, BaseCanvas canvas, bool reassignment = false)
        {
            string[] ops = parts[1].Split(',');
            // Set the current working directory to the directory where the executable is located
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            // Specify the source file
            string sourceImagePath = Path.Combine("C:\\Users\\IO\\source\\repos\\IOGPL\\IOGPL\\", ops[0].ToString());

            // Specify the destination file (where you want to copy the image in the executable directory)
            string destinationImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ops[0].ToString());

            try
            {
                // Copy the file to the destination
                File.Copy(sourceImagePath, destinationImagePath, true);
                int x = ResolveValue(ops[1]);
                int y = ResolveValue(ops[2]);

                Console.WriteLine("File copied successfully.");
                Icon newIcon = new Icon(sourceImagePath);
                
                canvas.CreateIcon(newIcon, x, y);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error copying file: {ex.Message}");
            }

            
        }

        private int ResolveValue(string input)
        {
            
            if (proto.variables.ContainsKey(input))
            {
                return proto.variables[input];
            }

            if (int.TryParse(input, out int constantValue))
            {
                return constantValue;
            }

            Console.WriteLine($"Invalid value: {input}");
            return 0;
        }
    }
}
