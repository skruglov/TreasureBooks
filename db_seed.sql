CREATE TABLE Authors(AuthorId INT IDENTITY PRIMARY KEY, Name varchar(255));

CREATE TABLE Books(
	BookId INT IDENTITY PRIMARY KEY, 
	Title varchar(255) not null,
	Genre varchar(255),
	Price decimal(18,2),
	AuthorId int not null REFERENCES Authors(AuthorId)
);

insert Authors(Name) values('Jane Austen');
insert Authors(Name) values('Charles Dickens');
insert Authors(Name) values('Miguel de Cervantes');

insert Books(Title,Price,Genre,AuthorId) values('Pride and Prejudice', 9.99, 'Comedy of manners', 1);
insert Books(Title,Price,Genre,AuthorId) values('Northanger Abbey', 12.95, 'Gothic parody', 1);
insert Books(Title,Price,Genre,AuthorId) values('David Copperfield', 15, 'Bildungsroman', 2);
insert Books(Title,Price,Genre,AuthorId) values('Don Quixote', 8.95, 'Picaresque', 3);


/*
drop table Books
drop table Authors
*/
