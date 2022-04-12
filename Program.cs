using System.IO;


string dir = @"C:\Users\brend\Documents\folda";
string path = "";

List<string> files = new List<string>();

try
{
    Directory.SetCurrentDirectory(dir);
    files = Directory.GetFiles(dir, "theMachineStops.txt").ToList();
    
    if (files.Count > 0)
    {
        path = files[0];
    }
}
catch (IOException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}


PeriodToStop(path, dir);

static void PeriodToStop(string path, string dir)
{
    FileInfo file = new FileInfo(path);

    if (file.Exists)
    {
        try
        {
            string newTxt = "";
            using (StreamReader reader = file.OpenText())
            {
                string text = reader.ReadToEnd();

                string[] split = text.Split('.');

                foreach (string part in split)
                {
                    newTxt += part + "STOP";
                }
            }

            string telegramCopyPath = dir + @"\TelegramCopy.txt";

            if (File.Exists(telegramCopyPath))
            {
                Console.WriteLine("File already exists");
                return;
            }

            using (StreamWriter writer = File.CreateText(telegramCopyPath))
            {
                writer.Write(newTxt);
            }

            Console.WriteLine("Success");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

