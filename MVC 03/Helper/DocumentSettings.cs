namespace MVC_03.Helper
{
    public class DocumentSettings
    {
        //1. Upload
        public static string Upload(IFormFile file,string folderName)
        {
            // 1. Get Location Of folder
            // string folderPath = $"D:\\.Net Route\\Eng Ahmed Amin\\07 ASP.NET Core MVC\\Session 03\\MVC 03 Solution\\MVC 03\\wwwroot\\files\\{folderName}";
            //string folderPath = Directory.GetCurrentDirectory() + $"\\wwwroot\\files\\{folderName}";
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string folderPath =(CurrentDirectory + $"\\wwwroot\\files\\{folderName}");

            //2. Get FileName And Make It Unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. Get File Path : FolderPath + FileName
            string filePath = Path.Combine(folderPath, fileName);

            //4.File Stream
            using var FileStream = new FileStream(filePath,FileMode.Create,FileAccess.Write);

            // copyto deal with stream
            // file stream : Data per sec
            file.CopyTo(FileStream);

            return fileName;
        }


        //2. Delete 
        public static void Delete(string fileName ,  string folderName)
        {
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string filePath =(CurrentDirectory+ $"\\wwwroot\\files\\{folderName}\\"+fileName);
            //D:\.Net Route\Eng Ahmed Amin\07 ASP.NET Core MVC\Session 03\MVC 03 Solution\MVC 03\wwwroot\files\images\c4b0b435-9582-4680-804a-81ae424b0a76PicsArt_08-16-12.09.06.jpg
            if (File.Exists(filePath) )
            {
                File.Delete(filePath);
            }
            //D:\.Net Route\Eng Ahmed Amin\07 ASP.NET Core MVC\Session 03\MVC 03 Solution\MVC 03\wwwroot\files\images\3143d394-017b-47e8-a435-e2338bdb0141IMG_20231221_023608_578.webp
        }
    }
}
