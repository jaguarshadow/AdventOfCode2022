namespace Day11 {
    internal class Monkey {

        public static List<Monkey> Troop = new List<Monkey>();

        static int TroopLCM = 1;

        public int        Id;
        public char       Operation;
        public int        OpValue;
        public int        TestValue;
        public List<long> Items;
        public (int t, int f) ThrowTo;
        public long       NumInspected;

        public static Monkey Parse(List<string> monkeyinfo) {
            var monkey       = new Monkey();
            monkey.Id        = int.Parse(monkeyinfo[0].Substring(monkeyinfo[0].Length - 2, 1));
            monkey.Items     = monkeyinfo[1].Substring(monkeyinfo[1].IndexOf(":")  + 1).Split(",").Select(x => long.Parse(x)).ToList();
            var op           = monkeyinfo[2].Split(" ").TakeLast(2);
            monkey.Operation = op.First()[0];
            if (op.Last() == "old") monkey.OpValue = -1;
            else monkey.OpValue = int.Parse(op.Last());
            monkey.TestValue  = int.Parse(monkeyinfo[3].Split(" ").Last());
            TroopLCM *= monkey.TestValue;
            monkey.ThrowTo.t = int.Parse(monkeyinfo[4].Split(" ").Last());
            monkey.ThrowTo.f = int.Parse(monkeyinfo[5].Split(" ").Last());
            monkey.NumInspected = 0;
            return monkey;
        }

        public long InspectItem(long worry, bool partone) {
            NumInspected++;
            var operand = OpValue >= 0 ? OpValue : worry;
            if (Operation == '+') worry += operand;
            else if (Operation == '*') worry *= operand;
            if (partone) return worry / 3;
            else return worry % TroopLCM;
        }

        public void InspectAndThrowItems(bool partone = false) {
            foreach (var worry in Items) {
                var new_worry = InspectItem(worry, partone);
                if (new_worry % TestValue == 0) Troop[ThrowTo.t].Items.Add(new_worry);
                else Troop[ThrowTo.f].Items.Add(new_worry);
            }
            Items.Clear();
        }
    }
}
