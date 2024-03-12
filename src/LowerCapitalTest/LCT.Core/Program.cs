namespace LCT.Core
{
    public class ListNode
    {
        public ListNode Previous;
        public ListNode Next;
        public ListNode Random; // произвольный элемент внутри списка
        public string Data;
    }

    public class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(Stream stream)
        {
            Dictionary<ListNode, int> dic = new Dictionary<ListNode, int>();
            ListNode curNode = Head;
            for (int id = 0; curNode != null; curNode = curNode.Next, id++)
            {
                dic.Add(curNode, id);
            }
            var writer = new BinaryWriter(stream);
            curNode = Head;
            while (curNode != null)
            {
                writer.Write(curNode.Data);
                writer.Write(curNode.Random == null ? -2 : dic[curNode.Random]);
                curNode = curNode.Next;
            }
        }

        private ListNode GetNode(int id)
        {
            ListNode curNode = Head;
            for (int i = 0; curNode != null; curNode = curNode.Next, i++)
            {
                if (i == id) return curNode;
            }
            return new ListNode();
        }


        public void Deserialize(Stream stream)
        {
            using (var reader = new BinaryReader(stream))
            {
                stream.Seek(0, SeekOrigin.Begin);
                Head = null;
                Tail = null;
                Count = 0;
                ListNode curNode = null, prevNode = null;
                List<int> randomReferences = new List<int>();
                while (reader.PeekChar() != -1)
                {
                    curNode = new ListNode();
                    if (Count == 0) { Head = curNode; }
                    curNode.Data = reader.ReadString();
                    randomReferences.Add(reader.ReadInt32());
                    if (prevNode != null) { prevNode.Next = curNode; }
                    curNode.Previous = prevNode;
                    prevNode = curNode;
                    Count++;
                }
                Tail = curNode;
                for (int i = 0; i < Count; i++)
                {
                    GetNode(i).Random = randomReferences[i] == -2 ? null : GetNode(randomReferences[i]);
                }
            }
        }
    }
}

