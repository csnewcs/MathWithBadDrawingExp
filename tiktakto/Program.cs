using System;
using System.Drawing;

namespace tiktakto
{
    enum Team
    {
        _, O, X
    }
    class MiniPan
    {
        Team[,] ate;
        Team own;
        bool eaten = false;
        public MiniPan()
        {
            ate = new Team[3,3] {
                {Team._, Team._, Team._},
                {Team._, Team._, Team._},
                {Team._, Team._, Team._}
            };
            own = Team._;
        }
        public Team ateTeam
        {
            get {
                return own;
            }
        }
        public bool wasEaten
        {
            get {
                return eaten;
            }
        }
        public bool put(Team team, Point pt)
        {
            if(ate[pt.X, pt.Y] != Team._)
            {
                return false;
            }
            else
            {
                ate[pt.X, pt.Y] = team;
                Console.WriteLine($"{pt.X}, {pt.Y}를 {team}이 먹음");
                
                check();
                foreach(var a in ate)
                {
                    Console.WriteLine(a);
                }
                return true;
            }
        }
        public Team[,] getAte
        {
            get {return ate;}
        }
        private void check()
        {                
            //가로줄, 세로줄 체크
            Team X = ate[0, 0];
            Team Y = ate[0, 0];
            bool xCorrect = true;
            bool yCorrect = true;
            for(int i = 0; i < 3; i++)
            {
                X = ate[i, 0];
                Y = ate[0, i];
                xCorrect = true;
                for(int j = 0; j < 3; j++)
                {
                    if(ate[i, j] != X)
                    {
                        xCorrect = false;
                    }
                    if(ate[j, i] != Y)
                    {
                        yCorrect = false;
                    }
                }
                if(xCorrect)
                {
                    own = X;
                    eaten = true;
                    return;
                }
                if(yCorrect)
                {
                    own = Y;
                    eaten = true;
                    return;
                }
            }
            if((ate[0, 0] == ate[1, 1] && ate[1, 1] == ate[2, 2]) || (ate[0,2] == ate[1, 1] && ate[1, 1] == ate[2, 0]))
            {
                eaten = true;
                own = ate[1, 1];
            }
            bool all = true;
            foreach(var a in ate)
            {
                if(a != Team._)
                {
                    all = false;
                    continue;
                }
            }
            if(all)
            {
                eaten = true;
            }
        }
    }
    class BigPan
    {
        Point _lastPut = new Point(-1, -1);
        public Point lastPut
        {
            get {return _lastPut;}
        }
        MiniPan[,] ate;
        Team _win;
        public Team win {
            get {
                return _win;
            }
        }
        bool _done = false;
        public bool done
        {
            get {
                return _done;
            }
        }
        public BigPan()
        {
            ate = new MiniPan[3, 3] {
                {new MiniPan(), new MiniPan(), new MiniPan()},
                {new MiniPan(), new MiniPan(), new MiniPan()},
                {new MiniPan(), new MiniPan(), new MiniPan()}
            };
        }
        public bool put(Team team, Point pt, Point panPt)
        {
            if(panPt != lastPut && lastPut != new Point(-1, -1) && !ate[lastPut.X, lastPut.Y].wasEaten)
            {
                throw new Exception("잘못된 판 선택");
            }
            bool asdf = ate[panPt.X, panPt.Y].put(team, pt);
            if(ate[pt.X, pt.Y].wasEaten)
            {
                _lastPut = new Point(-1, -1);
            }
            return asdf;
        }
        public void print()
        {
            string[] line = new string[19];
            string width = "+-+-+-+-+-+-+-+-+-+";
            
            line[0] = width;
            Team[,] bigAte = new Team[9, 9];
            for(int i = 0; i < 3; i++) //큰 판에서 작은 판 X좌표
            {
                for(int j = 0; j < 3; j++) //큰 판에서 작은 판 Y좌표
                {
                    Team[,] miniAte = ate[i,j].getAte;
                    for(int k = 0; k < 3; k++) //작은 판에서 팀 X좌표
                    {
                        for(int l = 0; l < 3; l++) //작은 판에서 팀 Y좌표
                        {
                            bigAte[i * 3 + k, j * 3 + l] = miniAte[k, l];
                        }
                    }
                }
            }
            line[1] = $"|{bigAte[0,0]}|{bigAte[0,1]}|{bigAte[0,2]}|{bigAte[0,3]}|{bigAte[0,4]}|{bigAte[0, 5]}|{bigAte[0, 6]}|{bigAte[0, 7]}|{bigAte[0, 8]}|";
            line[2] = width;
            line[3] = $"|{bigAte[1,0]}|{bigAte[1,1]}|{bigAte[1,2]}|{bigAte[1,3]}|{bigAte[1,4]}|{bigAte[1, 5]}|{bigAte[1, 6]}|{bigAte[1, 7]}|{bigAte[1, 8]}|";
            line[4] = width;
            line[5] = $"|{bigAte[2,0]}|{bigAte[2,1]}|{bigAte[2,2]}|{bigAte[2,3]}|{bigAte[2,4]}|{bigAte[2, 5]}|{bigAte[2, 6]}|{bigAte[2, 7]}|{bigAte[2, 8]}|";
            line[6] = width;
            line[7] = $"|{bigAte[3,0]}|{bigAte[3,1]}|{bigAte[3,2]}|{bigAte[3,3]}|{bigAte[3,4]}|{bigAte[3, 5]}|{bigAte[3, 6]}|{bigAte[3, 7]}|{bigAte[3, 8]}|";
            line[8] = width;
            line[9] = $"|{bigAte[4,0]}|{bigAte[4,1]}|{bigAte[4,2]}|{bigAte[4,3]}|{bigAte[4,4]}|{bigAte[4, 5]}|{bigAte[4, 6]}|{bigAte[4, 7]}|{bigAte[4, 8]}|";
            line[10] = width;
            line[11] = $"|{bigAte[5,0]}|{bigAte[5,1]}|{bigAte[5,2]}|{bigAte[5,3]}|{bigAte[5,4]}|{bigAte[5, 5]}|{bigAte[5, 6]}|{bigAte[5, 7]}|{bigAte[5, 8]}|";
            line[12] = width;
            line[13] = $"|{bigAte[6,0]}|{bigAte[6,1]}|{bigAte[6,2]}|{bigAte[6,3]}|{bigAte[6,4]}|{bigAte[6, 5]}|{bigAte[6, 6]}|{bigAte[6, 7]}|{bigAte[6, 8]}|";
            line[14] = width;
            line[15] = $"|{bigAte[7,0]}|{bigAte[7,1]}|{bigAte[7,2]}|{bigAte[7,3]}|{bigAte[7,4]}|{bigAte[7, 5]}|{bigAte[7, 6]}|{bigAte[7, 7]}|{bigAte[7, 8]}|";
            line[16] = width;
            line[17] = $"|{bigAte[8,0]}|{bigAte[8,1]}|{bigAte[8,2]}|{bigAte[8,3]}|{bigAte[8,4]}|{bigAte[8, 5]}|{bigAte[8, 6]}|{bigAte[8, 7]}|{bigAte[8, 8]}|";
            line[18] = width;
            foreach(string l in line)
            {
                Console.WriteLine(l);
            }
        }
        private void check()
        {
            //가로줄, 세로줄 체크
            Team X = ate[0, 0].ateTeam;
            Team Y = ate[0, 0].ateTeam;
            bool xCorrect = true;
            bool yCorrect = true;
            for(int i = 0; i < 3; i++)
            {
                X = ate[i, 0].ateTeam;
                Y = ate[0, i].ateTeam;
                xCorrect = true;
                for(int j = 0; j < 3; j++)
                {
                    if(ate[i, j].ateTeam != X)
                    {
                        xCorrect = false;
                    }
                    if(ate[j, i].ateTeam != Y)
                    {
                        yCorrect = false;
                    }
                }
                if(xCorrect)
                {
                    _win = X;
                    _done = true;
                    return;
                }
                if(yCorrect)
                {
                    _win = Y;
                    _done = true;
                    return;
                }
            }
            if((ate[0, 0] == ate[1, 1] && ate[1, 1] == ate[2, 2]) || (ate[0,2] == ate[1, 1] && ate[1, 1] == ate[2, 0]))
            {
                _done = true;
                _win = ate[1, 1].ateTeam;
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            BigPan pan = new BigPan();
            while(!pan.done)
            {
                Point panPoint = pan.lastPut;
                Team turn = Team.O;
                if(panPoint == new Point(-1, -1))
                {
                    while(true)
                    {
                        Console.WriteLine("둘 판의 X좌표를 입력하세요(1~3)");
                        int x = -1;
                        if (!int.TryParse(Console.ReadLine(), out x) || x < 1 || x > 3) {
                            Console.WriteLine("잘못된 값을 입력했습니다. 다시 해 주세요.");
                            continue;
                        }
                        x--;
                        int y = -1;
                        Console.WriteLine("둘 판의 Y좌표를 입력하세요(1~3)");
                        if (!int.TryParse(Console.ReadLine(), out y) || y < 1 || y > 3) {
                            Console.WriteLine("잘못된 값을 입력했습니다. 다시 해 주세요.");
                            continue;
                        }
                        y--;
                        panPoint = new Point(x, y);
                        break;
                    }

                }
                Point pt = new Point();
                while(true)
                {
                    Console.WriteLine("둘 곳의 X좌표를 입력하세요(1~3)");
                    int x = -1;
                    if(!int.TryParse(Console.ReadLine(), out x) || x < 1 || x > 3) {
                        Console.WriteLine("잘못된 값을 입력했습니다. 다시 해 주세요.");
                        continue;
                    }
                    x--;
                    Console.WriteLine("둘 곳의 Y좌표를 입력하세요(1~3)");
                    int y = -1;
                    if(!int.TryParse(Console.ReadLine(), out y) || y < 1 || y > 3)
                    {
                        Console.WriteLine("잘못된 값을 입력했습니다. 다시 해 주세요.");
                        continue;
                    }
                    y--;
                    pt = new Point(x, y);
                    break;
                }
                if(!pan.put(turn, pt, panPoint))
                {
                    Console.WriteLine("잘못된 곳을 선택하였습니다. 다시 해 주세요.");
                    continue;
                }
                pan.print();
                if(pan.done)
                {
                    Console.WriteLine($"게임이 종료되었습니다. '{pan.win}'가 이겼습니다");
                    break;
                }
                if(turn == Team.O) turn = Team.X;
                else turn = Team.O;
            }
            Console.WriteLine("종료하려면 아무 키나 누르세요");
            Console.ReadKey();
        }
    }
}
