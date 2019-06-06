using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

using Inspinia_MVC5.Models;
using Microsoft.Win32;

namespace Inspinia_MVC5.Controllers
{
    public class DashboardsController : Controller
    {
        [DllImport("gdi32", EntryPoint = "AddFontResource")]
        public static extern int AddFontResourceA(string lpFileName);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern int AddFontResource(string lpszFilename);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern int CreateScalableFontResource(uint fdwHidden, string
            lpszFontRes, string lpszFontFile, string lpszCurrentPath);

        /// <summary>
        /// Installs font on the user's system and adds it to the registry so it's available on the next session
        /// Your font must be included in your project with its build path set to 'Content' and its Copy property
        /// set to 'Copy Always'
        /// </summary>
        /// <param name="contentFontName">Your font to be passed as a resource (i.e. "myfont.tff")</param>
        private void RegisterFont(string contentFontName)
        {
            // Creates the full path where your font will be installed
            Directory.CreateDirectory("C:\\MY_FONT_LOC");
            var fontDestination = Path.Combine("C:\\MY_FONT_LOC", contentFontName);

            if (!System.IO.File.Exists(fontDestination))
            {
                // Copies font to destination
                System.IO.File.Copy(Path.Combine(Server.MapPath("/fonts"), contentFontName), fontDestination, false);

                // Retrieves font name
                // Makes sure you reference System.Drawing
                PrivateFontCollection fontCol = new PrivateFontCollection();
                fontCol.AddFontFile(fontDestination);
                var actualFontName = fontCol.Families[0].Name;

                //Add font
                AddFontResource(fontDestination);
                //Add registry entry   
               // Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", actualFontName, contentFontName, RegistryValueKind.String);
            }
        }

        static StudentInfo myInfo = new StudentInfo
        {
            FirstName = "Michael",
            LastName = "Owen",
            Month = 10,
            Year = 2018,
            Degree = "King",
            ProgramName = "Speed World"
        };

        public ActionResult Dashboard_1()
        {
            RegisterFont("Museo 500.otf");
            RegisterFont("Olde English Regular.ttf");
            return View(myInfo);
        }

        /*
        public ActionResult diploma()
        {
            var dir = Server.MapPath("/Images");
            var path = Path.Combine(dir, "p_big3.jpg");
            FileStream stream = new FileStream(path, FileMode.Open);
            FileStreamResult result = new FileStreamResult(stream, "image/jpeg");
            result.FileDownloadName = "image.jpeg";

            return result;
        }*/

        public ActionResult diploma_askworth()
        {
            Image image = Image.FromFile(Path.Combine(Server.MapPath("/Images"), "cover_final.png"));
            int width = image.Width;
            int height = image.Height;

            using (Graphics g = Graphics.FromImage(image))
            {
                // do something with the Graphics (eg. write "Hello World!")
                string grad_str = string.Format("has been awarded {0}  {1} the {2} in", myInfo.Month, myInfo.Year, myInfo.Degree);
                string name_str = string.Format("{0} {1}", myInfo.FirstName, myInfo.LastName);
                string prog_str = myInfo.ProgramName;
                string comm_str = "hereby certifies that";

                // Create font and brush.
                Font nameFont = new Font("Olde English", 60);
                SolidBrush nameBrush = new SolidBrush(Color.FromArgb(35,21,14));

                Font gradFont = new Font("Museo 500", 18);
                SolidBrush gradBrush = new SolidBrush(Color.FromArgb(35, 21, 14));

                Font progFont = new Font("Olde English", 40);
                SolidBrush progBrush = new SolidBrush(Color.FromArgb(154, 27, 31));

                Font commFont = new Font("Museo 500", 18);
                SolidBrush commBrush = new SolidBrush(Color.FromArgb(35, 21, 14));

                // Create point for upper-left corner of drawing.
                RectangleF commRect = new RectangleF(new Point(0, height/4+16), new SizeF(width, 50));
                RectangleF nameRect = new RectangleF(new Point(0, height/4+60), new SizeF(width, 100));
                RectangleF gradRect = new RectangleF(new Point(0, height/2-16), new SizeF(width, 50));
                RectangleF progRect = new RectangleF(new Point(0, height*4/7-18), new SizeF(width, 100));

                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;

                g.DrawString(grad_str, gradFont, nameBrush, gradRect, format);
                g.DrawString(name_str, nameFont, gradBrush, nameRect, format);
                g.DrawString(prog_str, progFont, progBrush, progRect, format);
                g.DrawString(comm_str, commFont, commBrush, commRect, format);
            }

            MemoryStream ms = new MemoryStream();

            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return File(ms.ToArray(), "image/jpeg", myInfo.LastName +  ".jpeg");
        }

        public ActionResult diploma_james()
        {
            Image image = Image.FromFile(Path.Combine(Server.MapPath("/Images"), "second_cover.png"));
            int width = image.Width;
            int height = image.Height;

            using (Graphics g = Graphics.FromImage(image))
            {
                // do something with the Graphics (eg. write "Hello World!")
                string grad_str = string.Format("has been awarded {0}  {1} the {2} a", myInfo.Month, myInfo.Year, myInfo.Degree);
                string name_str = string.Format("{0} {1}", myInfo.FirstName, myInfo.LastName);
                string prog_str = myInfo.ProgramName;
                string comm_str = "hereby certifies that";

                // Create font and brush.
                Font nameFont = new Font("Olde English", 60);
                SolidBrush nameBrush = new SolidBrush(Color.FromArgb(35, 21, 14));

                Font gradFont = new Font("Museo 500", 18);
                SolidBrush gradBrush = new SolidBrush(Color.FromArgb(35, 21, 14));

                Font progFont = new Font("Olde English", 40);
                SolidBrush progBrush = new SolidBrush(Color.FromArgb(86, 120, 212));

                Font commFont = new Font("Museo 500", 18);
                SolidBrush commBrush = new SolidBrush(Color.FromArgb(35, 21, 14));

                // Create point for upper-left corner of drawing.
                RectangleF commRect = new RectangleF(new Point(0, height / 4 + 16), new SizeF(width, 50));
                RectangleF nameRect = new RectangleF(new Point(0, height / 4 + 60), new SizeF(width, 100));
                RectangleF gradRect = new RectangleF(new Point(0, height / 2 - 16), new SizeF(width, 50));
                RectangleF progRect = new RectangleF(new Point(0, height * 4 / 7 - 18), new SizeF(width, 100));

                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;

                g.DrawString(grad_str, gradFont, nameBrush, gradRect, format);
                g.DrawString(name_str, nameFont, gradBrush, nameRect, format);
                g.DrawString(prog_str, progFont, progBrush, progRect, format);
                g.DrawString(comm_str, commFont, commBrush, commRect, format);
            }

            MemoryStream ms = new MemoryStream();

            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return File(ms.ToArray(), "image/jpeg", myInfo.LastName + ".jpeg");
        }

    }
}