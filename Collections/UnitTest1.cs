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
            Assert.AreEqual(nums.ToString(), ("[]"));
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            //Aggange
            var nums = new Collection<int>(7);
            //Act

            //Assert
            Assert.AreEqual(nums.ToString(), ("[7]"));
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            //Aggange
            var nums = new Collection<int>(7, 9);
            //Act

            //Assert
            Assert.AreEqual(nums.ToString(), ("[7, 9]"));
        }

        [Test]
        public void Test_Collection_Add()
        {
            //Aggange
            var nums = new Collection<int>(5);
            //Act
            nums.Add(6);
            //Assert
            Assert.AreEqual(nums.ToString(), ("[5, 6]"));
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            //Aggange
            var nums = new Collection<int>(11);
            //Act
            nums.AddRange(12, 13);
            //Assert
            Assert.AreEqual(nums.ToString(), ("[11, 12, 13]"));
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
                nums.RemoveAt(i);
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            //Aggange
            var nums = new Collection<int>(11,21);
            //Act
            var item0 = nums[0];
            var item1 = nums[1];
            //Assert
            Assert.AreEqual(item0, (11));
            Assert.AreEqual(item1, (21));
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
            nums.InsertAt(0, 1);
            nums.RemoveAt(1);
            var item0 = nums[0];
            var item1 = nums[1];    
            //Assert
            Assert.AreEqual(item0, (1));
            Assert.AreEqual(item1, (20));
            Assert.That(nums.ToString, Is.EqualTo("[1, 20, 30, 40]"));
        }

        [Test]
        public void Test_Collection_InsertAtStart()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30, 40);
            //Act
            nums.InsertAt(0, 1);
            //Assert
            Assert.AreEqual(nums[0].ToString(), ("1"));
            Assert.That(nums.ToString, Is.EqualTo("[1, 10, 20, 30, 40]"));
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
            Assert.AreEqual(nums[index].ToString(), ("1"));
            Assert.That(nums.ToString, Is.EqualTo("[10, 20, 1, 30, 40]"));
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
            Assert.AreEqual(nums[index].ToString(), ("1"));
            Assert.That(nums.ToString, Is.EqualTo("[10, 20, 30, 40, 1]"));
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30, 40);
            //Act
            nums.RemoveAt(0);
            //Assert
            Assert.AreEqual(nums[0].ToString(), ("20"));
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
            Assert.AreEqual(nums[index-1].ToString(), ("30"));
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
    }
}