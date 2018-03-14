using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstUnitTest.BL.Services.Ticket;
using FirstUnitTest.DA.Whatever.Ticket;

namespace FirstUnitTest.ConsoleMode
{
    /// <summary>
    /// Program
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args">輸入參數</param>
        private static void Main(string[] args)
        {
            ITicketRepository ticketRepository = new TicketRepository();
            
            ITicketValidator ticketValidator = new TicketValidator();

            ITicketService ticketService = new TicketService(ticketRepository, ticketValidator);

            ticketService.InsertSlave(1);

            Console.WriteLine("Insert Success....請按任意鍵結束程式");

            Console.ReadKey();
        }
    }
}
