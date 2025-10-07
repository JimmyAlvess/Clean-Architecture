using CleanArchMvc.Domain.Entities;
using Xunit;

namespace CleanArchMvc.Domain.Test
{
    public class TestEntity : Entity
    {
        public TestEntity(int id)
        {
            Id = id;
        }
    }

    public class EntityTest
    {
        [Fact]
        public void Entity_HaveIdProperty()
        {
            int expectedId = 1;

            var entity = new TestEntity(expectedId);

            Assert.Equal(expectedId, entity.Id);
            Assert.IsType<int>(entity.Id);
        }

        [Fact]
        public void Entity_IdBeReadable()
        {
            var entity = new TestEntity(1);
            int expectedId = 1;

            Assert.Equal(expectedId, entity.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(-1)]
        public void Entity_AcceptVariousIdValues(int id)
        {
            var entity = new TestEntity(id);

            Assert.Equal(id, entity.Id);
        }

        [Fact]
        public void Entity_BeAbstract()
        {
            Assert.True(typeof(Entity).IsAbstract);
        }

        [Fact]
        public void Entity_InheritFromObject()
        {
            var entity = new TestEntity(1);

            Assert.IsAssignableFrom<object>(entity);
        }
    }
}
