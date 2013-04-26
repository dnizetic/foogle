-- Database: foogle

-- DROP DATABASE foogle;

CREATE DATABASE foogle
  WITH OWNER = postgres
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'Croatian_Croatia.1250'
       LC_CTYPE = 'Croatian_Croatia.1250'
       CONNECTION LIMIT = -1;


create table title (
id serial primary key, 
title varchar(50)  not null);


create table category(
id serial primary key, 
name varchar(50) not null);

create table foogle_user (
id serial primary key, 
email varchar(50) unique not null, 
firstname varchar(50), 
lastname varchar(50), 
title integer references title(id) on delete restrict on update cascade, 
role char(1), 
activity char(1), 
confirmed boolean);

create table user_category(
category integer references category on delete restrict on update cascade,
foogle_user integer references foogle_user on delete restrict on update cascade, 
id serial, 
primary key(category, foogle_user));

create table skill (
id serial primary key, 
name varchar(50) not null, 
category integer references category on delete restrict on update cascade not null);

create table recommendation (
skill integer references skill,
teacher integer references foogle_user on delete restrict on update cascade, 
student integer references foogle_user on delete restrict on update cascade,
id serial,
 primary key(skill, teacher, student));


create table student (
id integer references foogle_user on delete restrict on update cascade primary key,
jmbag varchar(5) unique not null, 
gpa float not null, 
experience float);

create table experience (
id serial primary key, 
student integer references student(id) on delete restrict on update cascade, 
company varchar(50) not null, 
months_of_service integer not null);

create function years_of_service() returns trigger as $$ 
declare months integer; 
years float; 
begin 
select cast((select sum(months_of_service) from experience where student=new.student) as float)/12 into years ;
update student set experience=years where id=new.student; 
return null; 
end; 
$$language plpgsql;

create trigger years_of_service_insert after insert on experience for each row execute procedure years_of_service();



