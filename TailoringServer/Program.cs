using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailoringLibrary.Server;

namespace TailoringServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServerClass.Server("localhost", 8888);

            }
            catch (Exception ex)
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
