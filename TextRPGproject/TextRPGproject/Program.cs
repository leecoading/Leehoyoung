using System;
using System.Collections.Generic;
using System.Threading;
using TextRPGproject;

namespace TextRPGproject
{
    public class start
    {
        static public void Main(string[] args)
        {
            MainScreen(); // 프로그램 시작
        }

        static public void MainScreen()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다!");

            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점\n");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">> ");
            string inputName = Console.ReadLine();

            LookStatus lookstatus = new LookStatus(); // 상태 보기 클래스 호출

            if (inputName == "1")
            {
                Console.Clear();
                lookstatus.Status();
            }
            else if (inputName == "2")
            {
                Console.Clear();
                inventory inventory = new inventory(lookstatus);
                inventory.Equip();
            }
            else if (inputName == "3")
            {
                Console.Clear();
                Store store = new Store(lookstatus); // 상점 클래스 호출
                store.Storelist();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
                Console.WriteLine("아무 키나 누르면 다시 실행합니다.");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo != null)
                {
                    MainScreen(); // 인스턴스 생성 없이 직접 호출
                }
            }
        }
    }

    public class LookStatus
    {
        // 캐릭터의 상태 정보
        public string Name { get; set; } = "Chad";
        public string Job { get; set; } = "전사";
        public int Level { get; set; } = 1;
        public int Attack { get; set; } = 10;
        public int Defense { get; set; } = 5;
        public int Health { get; set; } = 100;
        public int Gold { get; set; } = 1500;

        public void Status()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("상태 보기");
            Console.ResetColor();

            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {Level}");
            Console.WriteLine($"{Name} ( {Job} )");
            Console.WriteLine($"공격력 : {Attack}");
            Console.WriteLine($"방어력 : {Defense}");
            Console.WriteLine($"체 력 : {Health}");
            Console.WriteLine($"Gold : {Gold} G");

            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">> ");
            string action = Console.ReadLine();

            if (action == "0")
            {
                Console.Clear();
                start.MainScreen(); // 인스턴스 생성 없이 MainScreen을 직접 호출
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
                Console.WriteLine("아무 키나 누르면 다시 실행합니다.");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo != null)
                {
                    Console.Clear();
                    Status();
                }
            }
        }
    }

    public class inventory
    {
        LookStatus lookStatus; // LookStatus 클래스의 변수 선언

        public inventory(LookStatus status)
        {
            lookStatus = status; // 전달된 인스턴스를 멤버 변수에 저장
        }

        public void Equip()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.ResetColor();

            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine(" - 1 무쇠갑옷        | 방어력 +5 | 무쇠로 만들어져 튼튼한 갑옷입니다.");
            Console.WriteLine(" - 2 스파르타의 창   | 공격력 +7 | 스파르타의 전사들이 사용했다는 전설의 창입니다");
            Console.WriteLine(" - 3 낡은 검         | 공격력 +2 | 쉽게 볼 수 있는 낡은 검입니다.\n");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">> ");

            string action = Console.ReadLine();

            if (action == "1")
            {
                Console.WriteLine("무쇠갑옷의 장착이 완료되었습니다.\n");
                Console.WriteLine("방어력 5가 증가하였습니다.");

                if (lookStatus != null) // null 확인
                {
                    lookStatus.Defense += 5; // LookStatus 인스턴스의 방어력 값 증가
                }

                Thread.Sleep(2000); // 2초 동안 대기
                Console.Clear();
                Equip();
            }
            else if (action == "2")
            {
                Console.WriteLine("스파르타의 창 장착이 완료되었습니다.\n");
                Console.WriteLine("공격력 7이 증가하였습니다.");

                if (lookStatus != null) // null 확인
                {
                    lookStatus.Attack += 7; // LookStatus 인스턴스의 공격력 값 증가
                }

                Thread.Sleep(2000); // 2초 동안 대기
                Console.Clear();
                Equip();
            }
            else if (action == "3")
            {
                Console.WriteLine("낡은 검 의 장착이 완료되었습니다.\n");
                Console.WriteLine("공격력이 2 증가하였습니다.");
                if (lookStatus != null) // null 확인
                {
                    lookStatus.Attack += 2; // LookStatus 인스턴스의 공격력 값 증가

                    Thread.Sleep(2000); // 2초 동안 대기
                    Console.Clear();
                    Equip();
                }
            }
            else if (action == "0")
            {
                Console.Clear();
                start.MainScreen();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
                Console.WriteLine("아무 키나 누르면 다시 실행합니다.");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo != null)
                {
                    Console.Clear();
                    Equip();
                }
            }
        }
    }

    public class Store
    {
        private LookStatus lookstatus; // LookStatus를 저장하기 위한 필드
        private List<Item> items = new List<Item>(); // 아이템 목록을 저장할 리스트

        // 생성자를 통해 LookStatus 인스턴스 받기
        public Store(LookStatus status)
        {
            lookstatus = status;
            InitializeItems(); // 아이템 목록 초기화
        }

        // 아이템 목록 초기화
        private void InitializeItems()
        {
            items.Add(new Item { Name = "수련자 갑옷", Defense = 5, Price = 1000, Description = "수련에 도움을 주는 갑옷입니다." });
            items.Add(new Item { Name = "무쇠갑옷", Defense = 9, Price = 1200, Description = "무쇠로 만들어져 튼튼한 갑옷입니다.", IsPurchased = true });
            items.Add(new Item { Name = "스파르타의 갑옷", Defense = 15, Price = 3500, Description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다." });
            items.Add(new Item { Name = "낡은 검", Attack = 2, Price = 600, Description = "쉽게 볼 수 있는 낡은 검 입니다." });
            items.Add(new Item { Name = "청동 도끼", Attack = 5, Price = 1500, Description = "어디선가 사용됐던거 같은 도끼입니다." });
            items.Add(new Item { Name = "스파르타의 창", Attack = 7, Price = 2000, Description = "스파르타의 전사들이 사용했다는 전설의 창입니다.", IsPurchased = true });
        }

        // 상점 목록 표시
        public void Storelist()
        {
            Console.Clear();
            Console.WriteLine("**상점**");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{lookstatus.Gold} G\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                string purchaseStatus = item.IsPurchased ? "구매완료" : $"{item.Price} G";
                Console.WriteLine($"- {i + 1} {item.Name} | 공격력 +{item.Attack} | 방어력 +{item.Defense} | {item.Description} | {purchaseStatus}");
            }

            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("0. 나가기\n");

            Console.Write("원하시는 행동을 입력해주세요: ");
            string action = Console.ReadLine();

            if (action == "1")
            {
                BuyItem(); // 아이템 구매 기능 호출
            }
            else if (action == "0")
            {
                start.MainScreen(); // 메인 화면으로 이동
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 아무 키나 누르면 다시 실행합니다.");
                Console.ReadKey();
                Storelist(); // 다시 상점 목록 표시
            }
        }

        // 아이템 구매 기능
        public void BuyItem()
        {
            Console.Clear();
            Console.WriteLine("**상점 - 아이템 구매**");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{lookstatus.Gold} G\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                string purchaseStatus = item.IsPurchased ? "구매완료" : $"{item.Price} G";
                Console.WriteLine($"- {i + 1} {item.Name} | 공격력 +{item.Attack} | 방어력 +{item.Defense} | {item.Description} | {purchaseStatus}");
            }

            Console.WriteLine("\n0. 나가기");
            Console.Write("구매할 아이템 번호를 선택해주세요: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int itemNumber) && itemNumber >= 1 && itemNumber <= items.Count)
            {
                var selectedItem = items[itemNumber - 1];

                if (selectedItem.IsPurchased)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                }
                else if (lookstatus.Gold >= selectedItem.Price)
                {
                    Console.WriteLine("구매를 완료했습니다.");
                    lookstatus.Gold -= selectedItem.Price; // 골드 감소
                    selectedItem.IsPurchased = true; // 구매 상태로 변경
                    Thread.Sleep(1500); // 잠시 대기
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }
            else if (input == "0")
            {
                Storelist(); // 상점 목록으로 돌아가기
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Console.WriteLine("아무 키나 누르면 계속합니다.");
            Console.ReadKey();
            Storelist(); // 상점 목록으로 돌아가기
        }
    }

    // 아이템 클래스 정의
    public class Item
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool IsPurchased { get; set; } = false; // 구매 여부를 나타내는 변수
    }
}





