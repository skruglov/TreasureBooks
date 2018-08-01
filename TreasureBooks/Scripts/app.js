var ViewModel = function () {
    var self = this;

    self.isAdmin = ko.observable();
    function setIsAdmin() {
        self.isAdmin(window.location.hash === "#admin");
    }
    setIsAdmin();
    window.onhashchange = setIsAdmin;

    self.books = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.authors = ko.observableArray();

    self.newBook = {
        Author: ko.observable(),
        Genre: ko.observable(),
        Price: ko.observable(),
        Title: ko.observable()
    }

    self.detailForEdit = {
        BookId: ko.observable(),
        AuthorId: ko.observable(),
        Genre: ko.observable(),
        Price: ko.observable(),
        Title: ko.observable()
    }

    self.editing = ko.observable(false);
    

    var booksUri = '/api/books/';
    var authorsUri = '/api/authors/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllBooks() {
        ajaxHelper(booksUri, 'GET').done(function (data) {
            self.books(data);
        });
    }

    self.getBookDetail = function (item) {
        ajaxHelper(booksUri + item.BookId, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    function getAuthors() {
        ajaxHelper(authorsUri, 'GET').done(function (data) {
            self.authors(data);
        });
    }

    self.addBook = function (formElement) {
        var book = {
            AuthorId: self.newBook.Author().AuthorId,
            Genre: self.newBook.Genre(),
            Price: self.newBook.Price(),
            Title: self.newBook.Title()
        };

        ajaxHelper(booksUri, 'POST', book).done(function (item) {
            self.books.push(item);
        });
    }

    self.deleteBook = function (item) {
        ajaxHelper(booksUri + item.BookId, 'DELETE').done(function (deletedItem) {
            self.books.remove(function (data) { return data.BookId == deletedItem.BookId; });
        });
    }

    self.getBookDetailForEdit = function (item) {
        self.editing(true);
        ajaxHelper(booksUri + item.BookId, 'GET').done(function (data) {
            self.detailForEdit.BookId(data.BookId);
            self.detailForEdit.AuthorId(data.AuthorId);
            self.detailForEdit.Genre(data.Genre);
            self.detailForEdit.Price(data.Price);
            self.detailForEdit.Title(data.Title);
        });
    }

    self.editBook = function (formElement) {
        var book = {
            BookId: self.detailForEdit.BookId(),
            AuthorId: self.detailForEdit.AuthorId(),
            Genre: self.detailForEdit.Genre(),
            Price: self.detailForEdit.Price(),
            Title: self.detailForEdit.Title()
        };

        ajaxHelper(booksUri + book.BookId, 'PUT', book).done(function (item) {
            getAllBooks(); //update books

            //update book detail
            if (self.detail()) {
                if (book.BookId == self.detail().BookId) {
                    for (var i = 0; i < self.books().length; i++) {
                        if (book.BookId == self.books()[i].BookId) {
                            self.getBookDetail(self.books()[i]);
                            break;
                        }
                    }
                }
            }
        });
        
        self.editing(false);
    }

    self.cancelEdit = function (formElement) {
        self.editing(false);
    }

    // Fetch the initial data.
    getAllBooks();
    getAuthors();
};

ko.applyBindings(new ViewModel());