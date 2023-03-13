using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSavePrices.Services
{
    public class SendMailExecutor
    {
        private List<string> _paths;

        public SendMailExecutor()
        {

        }

        public SendMailExecutor(List<string> paths)
        {
            _paths = paths;
        }


        public void AddPath(string path)
        {
            _paths.Add(path);
            StartSendMails(path);
        }

        public void StartSendMails(string path)
        {
            Task.Run(() =>
            {

            });
        }

    }
}
