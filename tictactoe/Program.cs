using System;
using System.Drawing;

namespace tictactoe
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
                // Console.WriteLine($"{pt.X}, {pt.Y}를 {team}이 먹음");
                
                check();
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
                X = ate[0, i];
                Y = ate[i, 0];
                xCorrect = true;
                yCorrect = true;
                for(int j = 0; j < 3; j++)
                {
                    if(ate[j, i] == Team._ || ate[j, i] != X)
                    {
                        xCorrect = false;
                    }
                    if(ate[i, j] == Team._ || ate[i, j] != Y)
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
                if(ate[1, 1] != Team._)
                {
                    own = ate[1, 1];
                    eaten = true;
                }
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
                Console.WriteLine("판 설정 미스");
                return false;
            }
            else if(ate[panPt.X, panPt.Y].wasEaten)
            {
                Console.WriteLine("이미 먹힌 판");
                return false;
            }
            bool asdf = ate[panPt.X, panPt.Y].put(team, pt);
            _lastPut = pt;
            if(ate[pt.X, pt.Y].wasEaten)
            {
                _lastPut = new Point(-1, -1);
            }
            check();
            return asdf;
        }
        public void print()
        {
            string[] line = new string[23];
            string width = "+-+-+-+ +-+-+-+ +-+-+-+";
            
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
            line[1] = $"|{bigAte[0,0]}|{bigAte[1,0]}|{bigAte[2,0]}| |{bigAte[3,0]}|{bigAte[4,0]}|{bigAte[5, 0]}| |{bigAte[6, 0]}|{bigAte[7, 0]}|{bigAte[8, 0]}|";
            line[2] = width;
            line[3] = $"|{bigAte[0,1]}|{bigAte[1,1]}|{bigAte[2,1]}| |{bigAte[3,1]}|{bigAte[4,1]}|{bigAte[5, 1]}| |{bigAte[6, 1]}|{bigAte[7, 1]}|{bigAte[8, 1]}|";
            line[4] = width;
            line[5] = $"|{bigAte[0,2]}|{bigAte[1,2]}|{bigAte[2,2]}| |{bigAte[3,2]}|{bigAte[4,2]}|{bigAte[5, 2]}| |{bigAte[6, 2]}|{bigAte[7, 2]}|{bigAte[8, 2]}|";
            line[6] = width;
            line[7] = "";
            line[8] = width;
            line[9] = $"|{bigAte[0,3]}|{bigAte[1,3]}|{bigAte[2,3]}| |{bigAte[3,3]}|{bigAte[4,3]}|{bigAte[5, 3]}| |{bigAte[6, 3]}|{bigAte[7, 3]}|{bigAte[8, 3]}|";
            line[10] = width;
            line[11] = $"|{bigAte[0,4]}|{bigAte[1,4]}|{bigAte[2,4]}| |{bigAte[3,4]}|{bigAte[4,4]}|{bigAte[5, 4]}| |{bigAte[6, 4]}|{bigAte[7, 4]}|{bigAte[8, 4]}|";
            line[12] = width;
            line[13] = $"|{bigAte[0,5]}|{bigAte[1,5]}|{bigAte[2,5]}| |{bigAte[3,5]}|{bigAte[4,5]}|{bigAte[5, 5]}| |{bigAte[6, 5]}|{bigAte[7, 5]}|{bigAte[8, 5]}|";
            line[14] = width;
            line[15] = "";
            line[16] = width;
            line[17] = $"|{bigAte[0,6]}|{bigAte[1,6]}|{bigAte[2,6]}| |{bigAte[3,6]}|{bigAte[4,6]}|{bigAte[5, 6]}| |{bigAte[6, 6]}|{bigAte[7, 6]}|{bigAte[8, 6]}|";
            line[18] = width;
            line[19] = $"|{bigAte[0,7]}|{bigAte[1,7]}|{bigAte[2,7]}| |{bigAte[3,7]}|{bigAte[4,7]}|{bigAte[5, 7]}| |{bigAte[6, 7]}|{bigAte[7, 7]}|{bigAte[8, 7]}|";
            line[20] = width;
            line[21] = $"|{bigAte[0,8]}|{bigAte[1,8]}|{bigAte[2,8]}| |{bigAte[3,8]}|{bigAte[4,8]}|{bigAte[5, 8]}| |{bigAte[6, 8]}|{bigAte[7, 8]}|{bigAte[8, 8]}|";
            line[22] = width;
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
                X = ate[0, i].ateTeam;
                Y = ate[i, 0].ateTeam;
                xCorrect = true;
                yCorrect = true;
                for(int j = 0; j < 3; j++)
                {
                    if(ate[j, i].ateTeam != X || ate[j, i].ateTeam == Team._)
                    {
                        xCorrect = false;
                    }
                    if(ate[i, j].ateTeam != Y || ate[i, j].ateTeam == Team._)
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
                else if(yCorrect)
                {
                    _win = Y;
                    _done = true;
                    return;
                }
                
            }
            if(((ate[0, 0].ateTeam == ate[1, 1].ateTeam && ate[1, 1].ateTeam == ate[2, 2].ateTeam) || (ate[0,2].ateTeam == ate[1, 1].ateTeam && ate[1, 1].ateTeam == ate[2, 0].ateTeam)) && ate[1, 1].ateTeam != Team._)
            {
                _done = true;
                _win = ate[1, 1].ateTeam;
                return;
            }
            bool all = true;
            foreach(var pan in ate)
            {
                if(!pan.wasEaten)
                {
                    all = false;
                }
            }
            if(all) _done = true;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("게임을 시작합니다. 이 게임은 이상한 수학책에 나오는 궁극의 틱택토를 프로그램으로 만든 것입니다. 규칙은 해당 책의 그 부분을 봐주세요. 처음 시작 팀은 O입니다.\n좌표는 X는 왼쪽에서 오른쪽으로 갈 수록 커지고 Y는 위에서 아래로 갈 수록 커집니다.");
            BigPan pan = new BigPan();
            Team turn = Team.O;
            while(!pan.done)
            {
                Console.WriteLine($"{turn} 차례입니다.");
                Point panPoint = pan.lastPut;
                if(panPoint == new Point(-1, -1))
                {
                    // Console.WriteLine("이 판은 먹혔습니다. 새로운 판을 선택합니다.");
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
                Console.WriteLine($"둘 판의 좌표: ({panPoint.X+1}, {panPoint.Y+1})");
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
                    string winTeamExist = $"승리한 것은 '{pan.win}'";
                    string tie = "무승부";
                    string result = pan.win == Team._ ? tie : winTeamExist;
                    Console.WriteLine($"게임이 종료되었습니다. {result}입니다.");
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
