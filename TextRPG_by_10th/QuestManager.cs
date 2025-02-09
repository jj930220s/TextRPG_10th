using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_by_10th
{
    public  class QuestManager
    {
        private List<Quest> allQuest = new List<Quest>();
        private List<Quest> myQuest= new List<Quest>();

        private Inventory inven;

        public void ShowQuestList()
        {
            // 진행중인 모든 퀘스트 나열하기
            Console.Clear();
            Console.WriteLine("===== 퀘스트 =====");
            Console.WriteLine("현재 진행중인 퀘스트를 확인할 수 있습니다.\n");

            foreach (var item in myQuest)
            {
                ShowQuestDetail(item);
            }

            Console.WriteLine("\n1. 퀘스트 관리");
            Console.WriteLine("0. 나가기");
            Console.Write(">> ");
            string input = Console.ReadLine();

            if (input == "1")                       // 퀘스트관리
            {
                
            }
            else if (input == "0")                  // 메인 씬
            {
                SceneManager.instance.currentScene = SceneManager.Scene.Start;
                SceneManager.instance.GameScecne(SceneManager.Scene.Start);
                return;
            }

        }

        private void ShowQuestDetail(Quest quest)
        {
            int i = 1;

            Console.WriteLine($"{i++}. {quest.name}\t클리어 : {CheckQuestClear(quest)}");

            Console.WriteLine("퀘스트 클리어 조건 : ");
            foreach (var item in quest.miscItems)
            {
                Console.Write($"{item.Name} {item.Amount}개 \t");
            }
            Console.WriteLine();
            Console.WriteLine();

        }

        public void AddQuest(int index)
        {
            Quest q = allQuest.FirstOrDefault(x => x.index == index);

            if (myQuest.Contains(q))
            {
                Console.WriteLine("이미 가지고 있는 퀘스트입니다.");
                return;
            }
            
            myQuest.Add(q);
        }

        // 저장기능 넣을거면 json으로 가져오는 내용으로 교체 필요
        public void SetBasicQuest()
        {
            Quest quest1 = new Quest() { index = 1, name = "이름1", des = "설명1", canClear = false,
            miscItems=new List<MiscItem>()};

            MiscItem item = MiscItem.GetMiscCatalog().First();
            MiscItem item3 = MiscItem.GetMiscCatalog().ElementAt(2);
            item.Amount = 2;
            item3.Amount = 3;
            quest1.miscItems.Add(item);
            quest1.miscItems.Add(item3);


            Quest quest2 = new Quest() { index = 2, name = "이름2", des = "설명2", canClear = false,
                miscItems = new List<MiscItem>()};
        

            MiscItem item2 = MiscItem.GetMiscCatalog().First();
            item2.Amount = 7;
            quest2.miscItems.Add(item2);

            Quest quest3 = new Quest() { index = 3, name = "이름3", des = "설명3", canClear = false };
            allQuest.Add(quest1);
            allQuest.Add(quest2);
            allQuest.Add(quest3);


            // 보유 퀘스트 확인용 코드
            AddQuest(1);
            AddQuest(2);

            inven = SceneManager.instance.inventory;
        }
        
        private bool CheckQuestClear(Quest q)
        {
            List<MiscItem> list = q.miscItems;

            foreach(MiscItem item in list)
            {
                bool b = inven.GetMiscList().Where(x => x.Id == item.Id).Select(y => y.Amount >= item.Amount).FirstOrDefault();

                if (b)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

    }

    public class Quest 
    {
        public int index { get; set; }
        public string name { get; set; }
        public string des { get; set; }
        public bool canClear { get; set; }

        // 클리어 조건 데이터 필요
        // 보상 관련 데이터 필요


        // 임시 퀘스트클리어조건
        public List<MiscItem> miscItems { get; set; }

    }
}
