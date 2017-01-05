using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using KrosmagaUniverse.Models;
using PropertyChanged;
using System.Collections.ObjectModel;
using KrosmagaUniverse.KrosData;

namespace KrosmagaUniverse.PageModel
{
    [ImplementPropertyChanged]
    public class DeckBuilderPageModel : FreshBasePageModel
    {
        private short nbMaxCard = 3;
        private short nbMaxKrosmiqueCard = 1;
        private KrosCard lastCardSelected;
        private ControleurGererDataAccess controleurGererData; 
        public ClassModel DeckBuilderClass { get; set; }
        public List<KrosCard> CardsListToShow { get; set; }



        public ObservableCollection<KrosCard> CardsDeckList { get; set; }
        public Dictionary<KrosCard,int> CardsDeckListQuantity { get; set; }
        public DeckBuilderPageModel()
        {
           controleurGererData = new ControleurGererDataAccess();
        }
        public KrosCard CardToAddToDeck
        {
            get
            {
                return null;
            }
            set
            {
                if (bCanAddCard(value))
                {
                    
                    CardsDeckList.Add(value);
                    AddToCardsQuantity(value);
                    
                    //RaisePropertyChanged();
                    lastCardSelected = value;
                }
            }
        }

        private void AddToCardsQuantity(KrosCard value)
        {
          //  CardsDeckListQuantity[value]
        }

        private bool bCanAddCard(KrosCard value)
        {
            int nbCards = CardsDeckList.Count(x => x == value);
            switch (value.Rarity)
            {
                case 3:
                case 4:
                    if (nbCards == nbMaxKrosmiqueCard)
                        return false;
                    else
                        return true;
                    break;
                case 0:
                case 1:
                case 2:
                    if (nbCards == nbMaxCard)
                        return false;
                    else
                        return true;
                    break;
                    
                default:
                    return false;
                    break;
            }
            
        }


        public override void Init(object initData)
        {
            base.Init(initData);
            DeckBuilderClass = (ClassModel)initData;
            CardsDeckList = new ObservableCollection<KrosCard>();
            //Ajouter le test de la classe selectionnée. Switch Enum Class , pour charger les cartes de la classe.
            var list = new List<KrosCard>();
            list = controleurGererData.GetAllCardsByClass(DeckBuilderClass.IdClass);
            CardsListToShow = list;
      
            //LoadTwoCardsInDeck();

        }

        private void LoadTwoCardsInDeck()
        {
            CardsDeckList.Add(new KrosCard { CostAP = 3, MovementPoint = 4, Life = 5, Attack = 4, Name = "Betty Boubz", ImgFRURL = "http://i.imgur.com/TXiPWLk.png" });
            CardsDeckList.Add(new KrosCard { CostAP = 3, MovementPoint = 4, Life = 5, Attack = 4, Name = "clara_byne", ImgFRURL = "http://i.imgur.com/TXiPWLk.png" });

        }

        #region LoadCra
        //private void LoadCraCardList()
        //{
        //    var list = new List<KrosCard>();

        //    list = new List<KrosCard>();
        //    list.Add(new KrosCard { CostAP = 3, MovementPoint = 4, Life = 5, Attack = 4, Name = "Betty Boubz", Description = "", ImagePath = "http://i.imgur.com/TXiPWLk.png" } );
        //    list.Add(new CardModernModel { CostAP = 3, MovementPoint = 4, Life = 5, Attack = 4, Name = "clara_byne", Description = "", ImagePath = "http://i.imgur.com/TXiPWLk.png" });
        //    list.Add(new CardModernModel { CostAP = 5, MovementPoint = 3, Life = 7, Attack = 4, Name = "criblage", Description = "", ImagePath = "http://i.imgur.com/EnE5zKy.png" });
        //    list.Add(new CardModernModel { CostAP = 7, MovementPoint = 5, Life = 4, Attack = 4, Name = "dernier_recours", Description = "", ImagePath = "http://i.imgur.com/TtMIE9q.png" });
        //    list.Add(new CardModernModel { CostAP = 4, MovementPoint = 6, Life = 8, Attack = 4, Name = "detournement", Description = "", ImagePath = "http://i.imgur.com/sxqFVNm.png" });
        //    list.Add(new CardModernModel { CostAP = 8, MovementPoint = 2, Life = 2, Attack = 4, Name = "eksa_soth", Description = "Asparagus pee stink.", ImagePath = "http://i.imgur.com/jNuv8Pw.png" });
        //    list.Add(new CardModernModel { CostAP = 2, MovementPoint = 4, Life = 3, Attack = 4, Name = "esquive", Description = "Broccoli donice with fruit.", ImagePath = "http://i.imgur.com/ILrODPc.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 3, Life = 2, Attack = 4, Name = "fantome_cra", Description = "Avocadoing taste better.", ImagePath = "http://i.imgur.com/mwcJSpl.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 5, Life = 4, Attack = 4, Name = "fleche_chercheuse", Description = "Tvomit.", ImagePath = "http://i.imgur.com/pxKZ7m8.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 6, Life = 3, Attack = 4, Name = "fleche_criblante", Description = "Ases your pee stink.", ImagePath = "http://i.imgur.com/YlnfZWR.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 1, Life = 5, Attack = 4, Name = "fleche_d_immolation", Description = "esn’t play nice with fruit.", ImagePath = "http://i.imgur.com/qBlsMef.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 3, Life = 6, Attack = 4, Name = "fleche_de_recul", Description = "Avonothing taste better.", ImagePath = "http://i.imgur.com/WntZk0A.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 2, Life = 2, Attack = 4, Name = "fleche_destructrice", Description = "e vomit.", ImagePath = "http://i.imgur.com/Dz7mMOr.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 4, Life = 4, Attack = 4, Name = "fleche_explosive", Description = "Ases your pee stink.", ImagePath = "http://i.imgur.com/W1Rj2hC.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 3, Life = 3, Attack = 4, Name = "fleche_percante", Description = "Bro’t play nice with fruit.", ImagePath = "http://i.imgur.com/x0Y6Auw.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 5, Life = 5, Attack = 4, Name = "fleche_tempete", Description = "Avocothing taste better.", ImagePath = "http://i.imgur.com/8LTtHwP.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 6, Life = 6, Attack = 4, Name = "guy_yomtella", Description = "Tastes.", ImagePath = "http://i.imgur.com/zMIeHjK.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 2, Life = 3, Attack = 4, Name = "harcelement", Description = "Asparagur pee stink.", ImagePath = "http://i.imgur.com/mWtemyH.png" });
        //    list.Add(new CardModernModel { CostAP = 3, MovementPoint = 4, Life = 5, Attack = 4, Name = "Betty Boubz", Description = "zzz", ImagePath = "http://i.imgur.com/vHqvLC1.png" });
        //    list.Add(new CardModernModel { CostAP = 3, MovementPoint = 4, Life = 5, Attack = 4, Name = "clara_byne", Description = "", ImagePath = "http://i.imgur.com/TXiPWLk.png" });
        //    list.Add(new CardModernModel { CostAP = 5, MovementPoint = 3, Life = 7, Attack = 4, Name = "criblage", Description = "", ImagePath = "http://i.imgur.com/EnE5zKy.png" });
        //    list.Add(new CardModernModel { CostAP = 7, MovementPoint = 5, Life = 4, Attack = 4, Name = "dernier_recours", Description = "", ImagePath = "http://i.imgur.com/TtMIE9q.png" });
        //    list.Add(new CardModernModel { CostAP = 4, MovementPoint = 6, Life = 8, Attack = 4, Name = "detournement", Description = "", ImagePath = "http://i.imgur.com/sxqFVNm.png" });
        //    list.Add(new CardModernModel { CostAP = 8, MovementPoint = 2, Life = 2, Attack = 4, Name = "eksa_soth", Description = "Asparagus pee stink.", ImagePath = "http://i.imgur.com/jNuv8Pw.png" });
        //    list.Add(new CardModernModel { CostAP = 2, MovementPoint = 4, Life = 3, Attack = 4, Name = "esquive", Description = "Broccoli donice with fruit.", ImagePath = "http://i.imgur.com/ILrODPc.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 3, Life = 2, Attack = 4, Name = "fantome_cra", Description = "Avocadoing taste better.", ImagePath = "http://i.imgur.com/mwcJSpl.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 5, Life = 4, Attack = 4, Name = "fleche_chercheuse", Description = "Tvomit.", ImagePath = "http://i.imgur.com/pxKZ7m8.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 6, Life = 3, Attack = 4, Name = "fleche_criblante", Description = "Ases your pee stink.", ImagePath = "http://i.imgur.com/YlnfZWR.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 1, Life = 5, Attack = 4, Name = "fleche_d_immolation", Description = "esn’t play nice with fruit.", ImagePath = "http://i.imgur.com/qBlsMef.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 3, Life = 6, Attack = 4, Name = "fleche_de_recul", Description = "Avonothing taste better.", ImagePath = "http://i.imgur.com/WntZk0A.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 2, Life = 2, Attack = 4, Name = "fleche_destructrice", Description = "e vomit.", ImagePath = "http://i.imgur.com/Dz7mMOr.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 4, Life = 4, Attack = 4, Name = "fleche_explosive", Description = "Ases your pee stink.", ImagePath = "http://i.imgur.com/W1Rj2hC.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 3, Life = 3, Attack = 4, Name = "fleche_percante", Description = "Bro’t play nice with fruit.", ImagePath = "http://i.imgur.com/x0Y6Auw.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 5, Life = 5, Attack = 4, Name = "fleche_tempete", Description = "Avocothing taste better.", ImagePath = "http://i.imgur.com/8LTtHwP.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 6, Life = 6, Attack = 4, Name = "guy_yomtella", Description = "Tastes.", ImagePath = "http://i.imgur.com/zMIeHjK.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 2, Life = 3, Attack = 4, Name = "harcelement", Description = "Asparagur pee stink.", ImagePath = "http://i.imgur.com/mWtemyH.png" });
        //    list.Add(new CardModernModel { CostAP = 3, MovementPoint = 4, Life = 5, Attack = 4, Name = "Betty Boubz", Description = "zzz", ImagePath = "http://i.imgur.com/vHqvLC1.png" });
        //    list.Add(new CardModernModel { CostAP = 3, MovementPoint = 4, Life = 5, Attack = 4, Name = "clara_byne", Description = "", ImagePath = "http://i.imgur.com/TXiPWLk.png" });
        //    list.Add(new CardModernModel { CostAP = 5, MovementPoint = 3, Life = 7, Attack = 4, Name = "criblage", Description = "", ImagePath = "http://i.imgur.com/EnE5zKy.png" });
        //    list.Add(new CardModernModel { CostAP = 7, MovementPoint = 5, Life = 4, Attack = 4, Name = "dernier_recours", Description = "", ImagePath = "http://i.imgur.com/TtMIE9q.png" });
        //    list.Add(new CardModernModel { CostAP = 4, MovementPoint = 6, Life = 8, Attack = 4, Name = "detournement", Description = "", ImagePath = "http://i.imgur.com/sxqFVNm.png" });
        //    list.Add(new CardModernModel { CostAP = 8, MovementPoint = 2, Life = 2, Attack = 4, Name = "eksa_soth", Description = "Asparagus pee stink.", ImagePath = "http://i.imgur.com/jNuv8Pw.png" });
        //    list.Add(new CardModernModel { CostAP = 2, MovementPoint = 4, Life = 3, Attack = 4, Name = "esquive", Description = "Broccoli donice with fruit.", ImagePath = "http://i.imgur.com/ILrODPc.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 3, Life = 2, Attack = 4, Name = "fantome_cra", Description = "Avocadoing taste better.", ImagePath = "http://i.imgur.com/mwcJSpl.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 5, Life = 4, Attack = 4, Name = "fleche_chercheuse", Description = "Tvomit.", ImagePath = "http://i.imgur.com/pxKZ7m8.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 6, Life = 3, Attack = 4, Name = "fleche_criblante", Description = "Ases your pee stink.", ImagePath = "http://i.imgur.com/YlnfZWR.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 1, Life = 5, Attack = 4, Name = "fleche_d_immolation", Description = "esn’t play nice with fruit.", ImagePath = "http://i.imgur.com/qBlsMef.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 3, Life = 6, Attack = 4, Name = "fleche_de_recul", Description = "Avonothing taste better.", ImagePath = "http://i.imgur.com/WntZk0A.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 2, Life = 2, Attack = 4, Name = "fleche_destructrice", Description = "e vomit.", ImagePath = "http://i.imgur.com/Dz7mMOr.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 4, Life = 4, Attack = 4, Name = "fleche_explosive", Description = "Ases your pee stink.", ImagePath = "http://i.imgur.com/W1Rj2hC.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 3, Life = 3, Attack = 4, Name = "fleche_percante", Description = "Bro’t play nice with fruit.", ImagePath = "http://i.imgur.com/x0Y6Auw.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 5, Life = 5, Attack = 4, Name = "fleche_tempete", Description = "Avocothing taste better.", ImagePath = "http://i.imgur.com/8LTtHwP.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 6, Life = 6, Attack = 4, Name = "guy_yomtella", Description = "Tastes.", ImagePath = "http://i.imgur.com/zMIeHjK.png" });
        //    list.Add(new CardModernModel { CostAP = 1, MovementPoint = 2, Life = 3, Attack = 4, Name = "harcelement", Description = "Asparagur pee stink.", ImagePath = "http://i.imgur.com/mWtemyH.png" });

        //    CardsListToShow = list;

        //}
        #endregion

    }
}

