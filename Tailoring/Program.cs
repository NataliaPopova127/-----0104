using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailoringLibrary.Client;


namespace Tailoring
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ClientClass.CommunicateClient("localhost", 8888);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error!    " + ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
