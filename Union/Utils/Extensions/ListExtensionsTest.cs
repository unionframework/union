using System.Collections.Generic;
using NUnit.Framework;

namespace Union.Utils.Extensions
{
    [TestFixture]
    public class ListExtensionsTest
    {
        private class ClassA
        {
            public readonly string Field;

            public ClassA(string field)
            {
                Field = field;
            }

            public override bool Equals(object obj)
            {
                return obj is ClassA && (obj as ClassA).Field == Field;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        [Test]
        public void RandomItemTest()
        {
            var list = new List<ClassA> { new ClassA("111"), new ClassA("222"), new ClassA("111") };
            var randomItem = list.RandomItem(new ClassA("111"));
            Assert.AreNotEqual("111", randomItem.Field);
        }
    }
}