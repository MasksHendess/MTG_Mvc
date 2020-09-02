using MTG_Mvc.Models;
using MTG_Mvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTG_Mvc.Interface
{
    public interface IdecklistInterface
    {
        public List<decklist> GetAllDeckLists();

        public List<card> GetDeckList(int id);

        public string DeleteDeckList(int id);
        public decklist PostDeckList(string Decklist);
    }
}
