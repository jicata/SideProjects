namespace CircularQueue.Tests
{
    using System;
    using System.Linq;

    using NUnit.Framework;
    using CircularQueue;

    [TestFixture]
    public class UnitTestsCircularQueue
    {
        [Test]
        public void Enqueue_EmptyQueue_ShouldAddElement()
        {
            // Arrange
            var queue = new CircularQueue<int>();

            // Act
            queue.Enqueue(5);

            // Assert
            Assert.AreEqual(1, queue.Count);
        }

        [Test]
        public void EnqueueDeque_ShouldWorkCorrectly()
        {
            // Arrange
            var queue = new CircularQueue<string>();
            var element = "some value";

            // Act
            queue.Enqueue(element);
            var elementFromQueue = queue.Dequeue();

            // Assert
            Assert.AreEqual(0, queue.Count);
            Assert.AreEqual(element, elementFromQueue);
        }

        [Test]
        public void Dequeue_EmptyQueue_ThrowsException()
        {
            // Arrange
            var queue = new CircularQueue<int>();

            // Act


            // Assert: expect and exception
            Assert.Throws(typeof(InvalidOperationException), () => { queue.Dequeue(); });
        }

        [Test]
        public void EnqueueDequeue100Elements_ShouldWorkCorrectly()
        {
            // Arrange
            var queue = new CircularQueue<int>();
            int numberOfElements = 1000;

            // Act
            for (int i = 0; i < numberOfElements; i++)
            {
                queue.Enqueue(i);
            }

            // Assert
            for (int i = 0; i < numberOfElements; i++)
            {
                Assert.AreEqual(numberOfElements - i, queue.Count);
                var element = queue.Dequeue();
                Assert.AreEqual(i, element);
                Assert.AreEqual(numberOfElements - i - 1, queue.Count);
            }
        }

        [Test]
        public void DequeueElement_ShouldRemoveCorrectElement()
        {
            // Arrange
            var queue = new CircularQueue<int>();
            var expectedQueue = new CircularQueue<int>();
            int nums = 10;
            int index = 0;
            for (int i = 1; i <= nums; i++)
            {
                queue.Enqueue(i);
                if (i > 1)
                {
                    expectedQueue.Enqueue(i);
                }
            }

            // Act
            queue.Dequeue();

            // Assert

            var resultArr = queue.ToArray();
            for (int i = 2; i <= nums; i++)
            {
                Assert.AreEqual(i,resultArr[index++],"Dequeue removed the incorrect element");
            }
        }

        [Test]
        public void CircularQueue_EnqueueDequeueManyChunks_ShouldWorkCorrectly()
        {
            // Arrange
            var queue = new CircularQueue<int>();
            int chunks = 100;

            // Act & Assert in a loop
            int value = 1;
            for (int i = 0; i < chunks; i++)
            {
                Assert.AreEqual(0, queue.Count);
                var chunkSize = i + 1;
                for (int counter = 0; counter < chunkSize; counter++)
                {
                    Assert.AreEqual(value - 1, queue.Count);
                    queue.Enqueue(value);
                    Assert.AreEqual(value, queue.Count);
                    value++;
                }
                for (int counter = 0; counter < chunkSize; counter++)
                {
                    value--;
                    Assert.AreEqual(value, queue.Count);
                    queue.Dequeue();
                    Assert.AreEqual(value - 1, queue.Count);
                }
                Assert.AreEqual(0, queue.Count);
            }
        }

        [Test]
        public void Enqueue500Elements_ToArray_ShouldWorkCorrectly()
        {
            // Arrange
            var array = Enumerable.Range(1, 500).ToArray();
            var queue = new CircularQueue<int>();

            // Act
            for (int i = 0; i < array.Length; i++)
            {
                queue.Enqueue(array[i]);
            }
            var arrayFromQueue = queue.ToArray();

            // Assert
            CollectionAssert.AreEqual(array, arrayFromQueue);
        }

        [Test]
        public void InitialCapacity1_EnqueueDequeue20Elements_ShouldWorkCorrectly()
        {
            // Arrange
            int elementsCount = 20;
            int initialCapacity = 1;

            // Act
            var queue = new CircularQueue<int>(initialCapacity);
            for (int i = 0; i < elementsCount; i++)
            {
                queue.Enqueue(i);
            }

            // Assert
            Assert.AreEqual(elementsCount, queue.Count);
            for (int i = 0; i < elementsCount; i++)
            {
                var elementFromQueue = queue.Dequeue();
                Assert.AreEqual(i, elementFromQueue);
            }
            Assert.AreEqual(0, queue.Count);
        }
    }
}
