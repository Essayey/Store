using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Store.Tests
{
    public class BookServiceTest
    {
        [Fact]
        public void GetAllByQuery_WithIsbn_CallsGetAllByIsbn()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();
            bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                              .Returns(new[] { new Book(1, "", "", "") });
            bookRepositoryStub.Setup(x => x.GetAllByTitleOrAutrhor(It.IsAny<string>()))
                              .Returns(new[] { new Book(2, "", "", "") });
            var bookService = new BookService(bookRepositoryStub.Object);
            var validIsbn = "ISBN 12345-12345";

            var actual = bookService.GetAllByQuery(validIsbn);
            Assert.Collection(actual, book => Assert.Equal(1, book.Id));
        }

        [Fact]
        public void GetAllByQuery_WithAuthor_CallsGetAllByTitleOrAutrhor()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();
            bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                              .Returns(new[] { new Book(1, "", "", "") });
            bookRepositoryStub.Setup(x => x.GetAllByTitleOrAutrhor(It.IsAny<string>()))
                              .Returns(new[] { new Book(2, "", "", "") });
            var bookService = new BookService(bookRepositoryStub.Object);
            var invalidIsbn = "12345-12345";

            var actual = bookService.GetAllByQuery(invalidIsbn);
            Assert.Collection(actual, book => Assert.Equal(2, book.Id));
        }
    }
}
