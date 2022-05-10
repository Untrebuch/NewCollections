using System;
using System.Linq;
using Collections;
using NUnit.Framework;

namespace CollectionTests
{
    public class Tests
    {

        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            //Aggange
            var nums = new Collection<int>();
            //Act

            //Assert
            Assert.AreEqual(0, nums.Count);
            Assert.AreEqual(16, nums.Capacity);
            Assert.AreEqual(("[]"), nums.ToString());
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            //Aggange
            var nums = new Collection<int>(7);
            //Act

            //Assert
            Assert.AreEqual(("[7]"), nums.ToString());
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            //Aggange
            var nums = new Collection<int>(7, 9);
            //Act

            //Assert
            Assert.AreEqual(("[7, 9]"), nums.ToString());
        }

        [Test]
        public void Test_Collection_Add()
        {
            //Aggange
            var nums = new Collection<int>(5);
            //Act
            nums.Add(6);
            //Assert
            Assert.AreEqual(("[5, 6]"), nums.ToString());
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            //Aggange
            var nums = new Collection<int>(11);
            //Act
            nums.AddRange(12, 13);
            //Assert
            Assert.AreEqual(("[11, 12, 13]"), nums.ToString());
        }

        [Test]
        public void Test_Collection_AddRangeWithGrowth()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            } 
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            //Aggange
            var nums = new Collection<int>(11, 21);
            //Act
            
            //Assert
            Assert.AreEqual(11, nums[0]);
            Assert.AreEqual(21, nums[1]);
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            var names = new Collection<string>("Bob", "Joe");
            Assert.That(() => { var name = names[-1]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Bob, Joe]"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            //Aggange
            var nums = new Collection<int>(10, 20, 30, 40);
            //Act
            nums[1] = 1;
            //Assert
            Assert.AreEqual(10, nums[0]);
            Assert.AreEqual(1, nums[1]);
            Assert.That(nums.ToString(), Is.EqualTo("[10, 1, 30, 40]"));
        }

        [Test]
        public void Test_Collection_InsertAtStart()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30, 40);
            //Act
            nums.InsertAt(0, 1);
            //Assert
            Assert.AreEqual(("1"), nums[0].ToString());
            Assert.That(nums.ToString(), Is.EqualTo("[1, 10, 20, 30, 40]"));
        }

        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30, 40);
            int index = nums.Count / 2;
            //Act
            nums.InsertAt(index, 1);
            //Assert
            Assert.AreEqual(("1"), nums[index].ToString());
            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 1, 30, 40]"));
        }

        [Test]
        public void Test_Collection_InsertAtEnd()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30, 40);
            int index = nums.Count;
            //Act
            nums.InsertAt(index, 1);
            //Assert
            Assert.AreEqual(("1"), nums[index].ToString());
            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30, 40, 1]"));
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30, 40);
            //Act
            nums.RemoveAt(0);
            //Assert
            Assert.AreEqual(("20"), nums[0].ToString());
            Assert.That(nums.ToString, Is.EqualTo("[20, 30, 40]"));
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30, 40);
            int index = nums.Count - 1;
            //Act
            nums.RemoveAt(index);
            //Assert
            Assert.AreEqual(("30"), nums[index - 1].ToString());
            Assert.That(nums.ToString, Is.EqualTo("[10, 20, 30]"));
        }

        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30, 40);
            int index = nums.Count / 2;
            //Act
            nums.RemoveAt(index);
            //Assert
            Assert.That(nums.ToString, Is.EqualTo("[10, 20, 40]"));
        }

        [Test]
        public void Test_Collection_RemoveAll()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30, 40);
            int i = nums.Count;
            //Act
            while (i-- > 0)
            {
                nums.RemoveAt(i);
            }
            //Assert
            Assert.That(nums.ToString, Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_Clear()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30, 40);
            //Act
            nums.Clear();
            //Assert
            Assert.That(nums.ToString, Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ExchangeFristLast()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30, 40);
            var i = nums.Count - 1;
            //Act
            nums.Exchange(0, i);
            //Assert
            Assert.AreEqual(("[40, 20, 30, 10]"), nums.ToString());
        }

        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30, 40);
            var i = nums.Count / 2;
            //Act
            nums.Exchange(i, i + 1);
            //Assert
            Assert.AreEqual(("[10, 20, 40, 30]"), nums.ToString());
        }

        [TestCase("Peter", 0, "Peter")]
        [TestCase("Peter,Maria,Steve", 0, "Peter")]
        [TestCase("Peter,Maria,Steve", 1, "Maria")]
        [TestCase("Peter,Maria,Steve", 2, "Steve")]
        public void Test_Collection_GetByValidIndex (string data, int index, string expectedValue)
        {
            var items = new Collection<string>(data.Split(","));
            var item = index;
            Assert.AreEqual(expectedValue, items[item]);
        }
    }
}