using System;
using AutoMapper;
using Blog.Application.Common.Mappings;
using Blog.Domain;
using Xunit;

namespace Blog.Tests
{
    public class MappingTest
    {
        public class TestDto : IMapped<TestDomainClass>
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime Created { get; set; }
        }

        public class TestDomainClass
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime Created { get; set; }
            public DateTime? Updated { get; set; }
        }

        [Fact]
        public void MappingsAreCreatedAutomatically()
        {
            var config = new MapperConfiguration(conf =>
            {
                conf.AddProfile(new AssemblyMappingProfile(typeof(TestDto).Assembly));
            });
            var mapper = new Mapper(config);

            var post = new TestDomainClass() {Title = "Hey!", Content = "Great content!", Created = DateTime.Now};
            var dto = mapper.Map<TestDomainClass, TestDto>(post);

            Assert.Equal(dto.Title, post.Title);
            Assert.Equal(dto.Content, post.Content);
            Assert.Equal(dto.Created, post.Created);
            
            var mappedPost = mapper.Map<TestDomainClass>(dto);
            
            Assert.Equal(dto.Title, mappedPost.Title);
            Assert.Equal(dto.Content, mappedPost.Content);
            Assert.Equal(dto.Created, mappedPost.Created);
            Assert.Null(mappedPost.Updated);
        }
    }
}