using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOGPL
{
    public interface ICommand
    {
        void Execute();
    }

    public class CommandProcessor
    {
        private ICommand _command;
        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void ExecuteCommand()
        {
            if(_command != null)
            {
                _command.Execute();
            }
        }
    }

    public class MoveToCommand : ICommand 
    {
        private PictureBox pictureBox;

        private int targetX, targetY;


        public MoveToCommand(PictureBox pictureBox,int targetX, int targetY)
        {
            this.pictureBox = pictureBox;
            this.targetX = targetX;
            this.targetY = targetY;

        }
        public void Execute() 
        {
            pictureBox.Image?.Dispose();
            Bitmap updatedImage = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics g = Graphics.FromImage(updatedImage))
            {
                // Draw the new ellipse at the target position
                g.FillEllipse(Brushes.Black, targetX, targetY, 5, 5);
                
            }

            // Update the PictureBox to reflect the changes
           
            // Update the default position to the new position
            pictureBox.Image = updatedImage;
            
        }
    }
}
