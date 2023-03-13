using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoSavePrices
{
    public class QAllClients
    {
        private List<Client> _allClients;

        private readonly int maxClientsOneThread;
       

        public QAllClients(int maxClientsOneThread)
        {
            this.maxClientsOneThread = maxClientsOneThread;
            _allClients = new List<Client>();
        }

        public int countClients => _allClients.Count;

        public void Add(Client newClient)
        {
            _allClients.Add(newClient);
        }

        /// <summary>
        /// Взять след.клиента
        /// </summary>
        /// <returns></returns>
        public Client Next()
        {
            try
            {
                if (_allClients.Count == 0)
                    throw new NullReferenceException("Пустой список клиентов");
                Client NextClient = _allClients.First();
                _allClients.RemoveAt(0);

                return NextClient;
            }
            catch (Exception ex)
            {
                UniLogger.WriteLog("Ошибка при взятии клиента", 1, ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Взять первых клиентов
        /// </summary>
        /// <returns></returns>
        public Client[] GetClients()
        {
            if (_allClients.Count == 0)
                throw new NullReferenceException("Пустой список клиентов");
            Client[] resultClients = _allClients.Take(maxClientsOneThread).ToArray();
            _allClients.RemoveRange(0, maxClientsOneThread);

            return resultClients;
        }

    }
}
