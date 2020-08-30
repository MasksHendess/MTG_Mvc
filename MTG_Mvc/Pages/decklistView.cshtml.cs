using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MTG_Mvc.Models;
using MTG_Mvc.Services;

namespace MTG_Mvc.Pages
{
    public class decklistModel : PageModel
    {
        public List<card> decklist { get; private set; }
        public decklistService deckListService { get; set; }
        public decklistModel(decklistService DeckListService)
        {
            deckListService = DeckListService;
        }
        public void OnGet()
        {
            decklist = deckListService.GetDecklist(1);
        }
    }
}
