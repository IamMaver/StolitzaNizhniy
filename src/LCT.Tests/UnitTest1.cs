using LCT.Core;

namespace LCT.Tests
{
    public class LctTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange

            ListNode ln1 = new ListNode();
            ListNode ln2 = new ListNode();
            ListNode ln3 = new ListNode();
            ListNode ln4 = new ListNode();

            ln1.Previous = null;
            ln1.Next = ln2;
            ln1.Random = null;
            ln1.Data = "String in 1st node";

            ln2.Previous = ln1;
            ln2.Next = ln3;
            ln2.Random = ln3;
            ln2.Data = "String in 2nd node";

            ln3.Previous = ln2;
            ln3.Next = ln4;
            ln3.Random = ln1;
            ln3.Data = "String in 3rd node";

            ln4.Previous = ln3;
            ln4.Next = null;
            ln4.Random = ln2;
            ln4.Data = "String in 4th node";

            ListRandom lr1 = new ListRandom();

            lr1.Head = ln1;
            lr1.Tail = ln4;
            lr1.Count = 4;

            Stream str = new MemoryStream();

            ListRandom lr2 = new ListRandom();

            // Act

            lr1.Serialize(str);

            lr2.Deserialize(str);

            // Assert

            Assert.Equal(4, lr2.Count);

            Assert.Equal(lr2.Head.Data, lr1.Head.Data);

            Assert.Equal(lr2.Tail.Random.Data, ln2.Data);
        }
    }
}