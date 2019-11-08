using System;

public class perl2Csharp
{
	public perl2Csharp()
	{
        // Case statement -- encode message based upon type
        // Common code. All messages start with the same 38 bits....

        ////////////////////////////////////////////////////////////
        // Bits Len   Description
        //    0-5       6   Message Type
        //    6-7       2   Repeat Indicator
        //    8-37     30   MMSI

        payload = sprintf("%06b", int type). "00".sprintf("%030b", int($mmsi))



        try
        {
            Console.WriteLine("binary message test");
            string authorName = "";
            int type = 8;
            string bookTitle = "ADO.NET Programming using C#";
            bool mvp = true;
            double price = 54.99;

            string fileName = @"C:\temp\MC.bin";
            BinaryWriter bwStream = new BinaryWriter(new FileStream(fileName, FileMode.Create));

            Encoding ascii = Encoding.ASCII;
            BinaryWriter bwEncoder = new BinaryWriter(new FileStream(fileName, FileMode.Create), ascii);

            using (BinaryWriter binWriter =
                new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                // Write string   
                binWriter.Write(authorName);
                // Write string   
                // Write integer  
                binWriter.Write(age);
                binWriter.Write(bookTitle);
                // Write boolean  
                binWriter.Write(mvp);
                // Write double   
                binWriter.Write(price);
            }
            Console.WriteLine("Data Written!");
            Console.WriteLine();
        }
        catch (IOException ioexp)
        {
            Console.WriteLine("Error: {0}", ioexp.Message);
        }

    }
}
