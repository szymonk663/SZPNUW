CREATE USER "user" WITH
	LOGIN
	SUPERUSER
	CREATEDB
	CREATEROLE
	INHERIT
	NOREPLICATION
	CONNECTION LIMIT -1
	PASSWORD 'qwerty';

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

CREATE DATABASE szpnuw WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'en_US.utf8' LC_CTYPE = 'en_US.utf8';

ALTER DATABASE szpnuw OWNER TO "user";

\connect szpnuw

CREATE TABLE public.users (
	id SERIAL,
    login "varchar"(64) NOT NULL,
    password "varchar"(64) NOT NULL,
    usertype SMALLINT NOT NULL,
    firstname "varchar"(64) NOT NULL,
    lastname "varchar"(64) NOT NULL,
    pesel VARCHAR(11),
    dateofbirth TIMESTAMP,
    city "varchar"(64),
    address "varchar"(128),
    CONSTRAINT pk_users PRIMARY KEY(id)
);

CREATE TABLE public.students (
	id SERIAL,
    userid INTEGER NOT NULL,
	albumnumber VARCHAR(6) NOT NULL,
    CONSTRAINT pk_students PRIMARY KEY(id),
    CONSTRAINT ux_students UNIQUE (userid),
  	CONSTRAINT fk_users FOREIGN KEY (userid)
    REFERENCES public.users(id)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
    NOT DEFERRABLE
);

CREATE TABLE public.lecturers (
	id SERIAL,
    userid INTEGER NOT NULL,
    code VARCHAR(8) NOT NULL,
    CONSTRAINT pk_lecturers PRIMARY KEY(id),
    CONSTRAINT ux_lecturers UNIQUE (userid),
    CONSTRAINT fk_users FOREIGN KEY (userid)
    REFERENCES public.users(id)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
    NOT DEFERRABLE
);

CREATE TABLE public.semesters (
	id SERIAL,
    academicyear "varchar"(9) NOT NULL,
    semesternumber INTEGER NOT NULL,
    fieldofstudy "varchar"(32) NOT NULL,
    CONSTRAINT pk_semesters PRIMARY KEY(id)
);

CREATE TABLE public.studentsemester (
	studentid INTEGER NOT NULL,
    semesterid INTEGER NOT NULL,
    CONSTRAINT pk_studentsemester PRIMARY KEY(studentid, semesterid),
    CONSTRAINT fk_students FOREIGN KEY (studentid)
    REFERENCES public.students(id)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
    NOT DEFERRABLE,
    CONSTRAINT fk_semesters FOREIGN KEY (semesterid)
    REFERENCES public.semesters(id)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
    NOT DEFERRABLE
);

CREATE TABLE public.subjects (
	id serial,
    name "varchar"(64) NOT NULL,
    description text,
    leaderid INTEGER NOT NULL,
    CONSTRAINT pk_subjects PRIMARY KEY(id),
    CONSTRAINT fk_lecturers FOREIGN KEY (leaderid)
    REFERENCES public.lecturers(id)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
    NOT DEFERRABLE
);

CREATE TABLE public.subjectssemesters (
	id SERIAL,
    subjectid INTEGER NOT NULL,
    semesterid INTEGER NOT NULL,
    CONSTRAINT pk_subjectssemesters PRIMARY KEY(id),
    CONSTRAINT ux_subjectssemesters UNIQUE (subjectid, semesterid),
    CONSTRAINT fk_subjects FOREIGN KEY (subjectid)
    REFERENCES public.subjects(id)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
    NOT DEFERRABLE,
    CONSTRAINT fk_semesters FOREIGN KEY (semesterid)
    REFERENCES public.semesters(id)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
    NOT DEFERRABLE
);

CREATE TABLE public.projects (
	id SERIAL,
    topic "varchar"(256) NOT NULL,
    description TEXT,
    lecturerid INTEGER NOT NULL,
    subjectid INTEGER NOT NULL,
	active BOOLEAN NOT NULL,
    CONSTRAINT pk_projects PRIMARY KEY(id),
    CONSTRAINT fk_lecturers FOREIGN KEY (lecturerid)
    REFERENCES public.lecturers(id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    NOT DEFERRABLE,
    CONSTRAINT fk_subjects FOREIGN KEY (subjectid)
    REFERENCES public.subjects(id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    NOT DEFERRABLE
);

CREATE TABLE public.sections (
	id SERIAL,
    sectionnumber INTEGER NOT NULL,
    subcjetsemesterid INTEGER NOT NULL,
    projectid INTEGER,
    CONSTRAINT pk_sections PRIMARY KEY(id),
    CONSTRAINT ux_sections UNIQUE (subcjetsemesterid, projectid),
    CONSTRAINT fk_subjectssemesters FOREIGN KEY (subcjetsemesterid)
    REFERENCES public.subjectssemesters(id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    NOT DEFERRABLE,
    CONSTRAINT fk_projects FOREIGN KEY (projectid)
    REFERENCES public.projects(id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    NOT DEFERRABLE
);

CREATE TABLE public.studentssections (
	id SERIAL,
    studentid INTEGER NOT NULL,
    sectionid INTEGER NOT NULL,
    rating REAL,
    dateofentry TIMESTAMP,
    CONSTRAINT pk_studentssections PRIMARY KEY(id),
    CONSTRAINT ux_studentssections UNIQUE (studentid, sectionid),
    CONSTRAINT fk_students FOREIGN KEY (studentid)
    REFERENCES public.students(id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    NOT DEFERRABLE,
    CONSTRAINT fk_sections FOREIGN KEY (sectionid)
    REFERENCES public.sections(id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    NOT DEFERRABLE
);

CREATE TABLE public.reports (
	id SERIAL,
    filename "varchar"(64) NOT NULL,
    content BYTEA NOT NULL,
    sectionid INTEGER NOT NULL,
    CONSTRAINT pk_raports PRIMARY KEY(id),
    CONSTRAINT fk_sections FOREIGN KEY (sectionid)
    REFERENCES public.sections(id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    NOT DEFERRABLE
);

CREATE TABLE public.meetings (
	id SERIAL,
    studentssectionsid INTEGER NOT NULL,
    dateofentry TIMESTAMP,
    rating REAL,
    CONSTRAINT pk_meetings PRIMARY KEY(id),
    CONSTRAINT fk_studentssections FOREIGN KEY (studentssectionsid)
    REFERENCES public.studentssections(id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    NOT DEFERRABLE
);

CREATE TABLE public.syslog (
	id SERIAL,
    name "varchar"(64) NOT NULL,
    details text,
    date TIMESTAMP NOT NULL,
    CONSTRAINT pk_syslog PRIMARY KEY(id)
);

INSERT INTO 
  public.users
(
  login,
  password,
  usertype,
  firstname,
  lastname,
  pesel,
  dateofbirth,
  city,
  address
)
VALUES (
  'admin',
  '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5',
  3,
  'admin',
  'admin',
  '11111111111',
  CURRENT_DATE,
  'miejsce',
  'miejsce'
);

INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('instructor1', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie1', 'nazwisko1', '22222222221', CURRENT_DATE, 'city1', 'city1');
INSERT INTO  public.lecturers(userid, code) VALUES (2, '11111111');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('instructor2', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie2', 'nazwisko2', '22222222222', CURRENT_DATE, 'city2', 'city2');
INSERT INTO  public.lecturers(userid, code) VALUES (3, '11111112');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('instructor3', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie3', 'nazwisko3', '22222222223', CURRENT_DATE, 'city3', 'city3');
INSERT INTO  public.lecturers(userid, code) VALUES (4, '11111113');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('instructor4', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie4', 'nazwisko4', '22222222224', CURRENT_DATE, 'city4', 'city4');
INSERT INTO  public.lecturers(userid, code) VALUES (5, '11111114');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('instructor5', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie5', 'nazwisko5', '22222222225', CURRENT_DATE, 'city5', 'city5');
INSERT INTO  public.lecturers(userid, code) VALUES (6, '11111115');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('instructor6', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie6', 'nazwisko6', '22222222226', CURRENT_DATE, 'city6', 'city6');
INSERT INTO  public.lecturers(userid, code) VALUES (7, '11111116');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('instructor7', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie7', 'nazwisko7', '22222222227', CURRENT_DATE, 'city7', 'city7');
INSERT INTO  public.lecturers(userid, code) VALUES (8, '11111117');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('instructor8', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie8', 'nazwisko8', '22222222228', CURRENT_DATE, 'city8', 'city8');
INSERT INTO  public.lecturers(userid, code) VALUES (9, '11111118');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('instructor9', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie9', 'nazwisko9', '22222222229', CURRENT_DATE, 'city9', 'city9');
INSERT INTO  public.lecturers(userid, code) VALUES (10, '11111119');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('instructor10', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie10', 'nazwisko10', '22222222231', CURRENT_DATE, 'city10', 'city10');
INSERT INTO  public.lecturers(userid, code) VALUES (11, '11111121');

INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 1, 'INFORMATYKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 2, 'INFORMATYKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 3, 'INFORMATYKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 4, 'INFORMATYKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 5, 'INFORMATYKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 6, 'INFORMATYKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 7, 'INFORMATYKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 1, 'ELEKTRONIKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 2, 'ELEKTRONIKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 3, 'ELEKTRONIKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 4, 'ELEKTRONIKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 5, 'ELEKTRONIKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 6, 'ELEKTRONIKA');
INSERT INTO public.semesters (academicyear, semesternumber, fieldofstudy) VALUES ('2017/2018', 7, 'ELEKTRONIKA');

INSERT INTO public.subjects ( name, description, leaderid) VALUES ('Nazwa1', 'opis1', '2');
INSERT INTO public.subjects ( name, description, leaderid) VALUES ('Nazwa2', 'opis2', '3');
INSERT INTO public.subjects ( name, description, leaderid) VALUES ('Nazwa3', 'opis3', '4');
INSERT INTO public.subjects ( name, description, leaderid) VALUES ('Nazwa4', 'opis4', '5');
INSERT INTO public.subjects ( name, description, leaderid) VALUES ('Nazwa5', 'opis5', '6');
INSERT INTO public.subjects ( name, description, leaderid) VALUES ('Nazwa6', 'opis6', '6');
INSERT INTO public.subjects ( name, description, leaderid) VALUES ('Nazwa7', 'opis7', '7');
INSERT INTO public.subjects ( name, description, leaderid) VALUES ('Nazwa8', 'opis8', '8');
INSERT INTO public.subjects ( name, description, leaderid) VALUES ('Nazwa9', 'opis9', '9');

INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 1, 1);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 2, 1);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 3, 1);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 4, 1);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 5, 1);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 6, 1);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 7, 1);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 8, 1);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 9, 1);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 1, 2);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 2, 3);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 3, 4);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 4, 5);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 5, 6);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 6, 7);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 7, 8);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 8, 9);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 9, 2);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 1, 3);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 2, 4);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 3, 5);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 4, 6);
INSERT INTO public.subjectssemesters( subjectid, semesterid) VALUES ( 5, 7);

INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('user1', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie1', 'nazwisko1', '12222222221', CURRENT_DATE, 'city1', 'city1');
INSERT INTO  public.students(userid, albumnumber) VALUES (12, '123456');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('user2', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie2', 'nazwisko2', '32222222222', CURRENT_DATE, 'city2', 'city2');
INSERT INTO  public.students(userid, albumnumber) VALUES (13, '223456');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('user3', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie3', 'nazwisko3', '42222222223', CURRENT_DATE, 'city3', 'city3');
INSERT INTO  public.students(userid, albumnumber) VALUES (14, '323456');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('user4', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie4', 'nazwisko4', '52222222224', CURRENT_DATE, 'city4', 'city4');
INSERT INTO  public.students(userid, albumnumber) VALUES (15, '423456');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('user5', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie5', 'nazwisko5', '62222222225', CURRENT_DATE, 'city5', 'city5');
INSERT INTO  public.students(userid, albumnumber) VALUES (16, '523456');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('user6', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie6', 'nazwisko6', '72222222226', CURRENT_DATE, 'city6', 'city6');
INSERT INTO  public.students(userid, albumnumber) VALUES (17, '623456');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('user7', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie7', 'nazwisko7', '82222222227', CURRENT_DATE, 'city7', 'city7');
INSERT INTO  public.students(userid, albumnumber) VALUES (18, '723456');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('user8', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie8', 'nazwisko8', '92222222228', CURRENT_DATE, 'city8', 'city8');
INSERT INTO  public.students(userid, albumnumber) VALUES (19, '823456');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('user9', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie9', 'nazwisko9', '11222222229', CURRENT_DATE, 'city9', 'city9');
INSERT INTO  public.students(userid, albumnumber) VALUES (20, '923456');
INSERT INTO public.users(login, password, usertype, firstname, lastname, pesel, dateofbirth, city, address) VALUES ('user10', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 2, 'imie10', 'nazwisko10', '21222222231', CURRENT_DATE, 'city10', 'city10');
INSERT INTO  public.students(userid, albumnumber) VALUES (21, '023456');

INSERT INTO public.studentsemester( studentid, semesterid) VALUES (1,1);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (2,1);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (3,1);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (4,1);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (5,1);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (6,1);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (7,1);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (1,2);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (2,2);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (3,2);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (4,2);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (5,2);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (6,2);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (7,2);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (1,3);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (2,3);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (3,3);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (4,3);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (5,3);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (6,3);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (7,3);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (1,4);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (2,4);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (3,4);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (4,4);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (5,4);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (6,4);
INSERT INTO public.studentsemester( studentid, semesterid) VALUES (7,4);

INSERT INTO public.projects( topic, description, lecturerid, subjectid, active) VALUES ( 'teamt1', 'opis1', 1, 1, true);
INSERT INTO public.projects( topic, description, lecturerid, subjectid, active) VALUES ( 'teamt2', 'opis2', 2, 1, true);
INSERT INTO public.projects( topic, description, lecturerid, subjectid, active) VALUES ( 'teamt3', 'opis3', 3, 1, true);
INSERT INTO public.projects( topic, description, lecturerid, subjectid, active) VALUES ( 'teamt4', 'opis4', 4, 1, true);
INSERT INTO public.projects( topic, description, lecturerid, subjectid, active) VALUES ( 'teamt5', 'opis5', 5, 1, true);
INSERT INTO public.projects( topic, description, lecturerid, subjectid, active) VALUES ( 'teamt6', 'opis6', 6, 1, true);
INSERT INTO public.projects( topic, description, lecturerid, subjectid, active) VALUES ( 'teamt7', 'opis7', 7, 1, true);
INSERT INTO public.projects( topic, description, lecturerid, subjectid, active) VALUES ( 'teamt8', 'opis8', 8, 1, true);
INSERT INTO public.projects( topic, description, lecturerid, subjectid, active) VALUES ( 'teamt9', 'opis9', 9, 1, true);

INSERT INTO public.sections( sectionnumber, subcjetsemesterid, projectid) VALUES (1, 1, 1);
INSERT INTO public.sections( sectionnumber, subcjetsemesterid, projectid) VALUES (1, 1, 2);
INSERT INTO public.sections( sectionnumber, subcjetsemesterid, projectid) VALUES (1, 1, 3);
INSERT INTO public.sections( sectionnumber, subcjetsemesterid, projectid) VALUES (1, 1, 4);
INSERT INTO public.sections( sectionnumber, subcjetsemesterid, projectid) VALUES (1, 1, 5);
INSERT INTO public.sections( sectionnumber, subcjetsemesterid, projectid) VALUES (1, 1, 6);
INSERT INTO public.sections( sectionnumber, subcjetsemesterid, projectid) VALUES (1, 1, 7);
INSERT INTO public.sections( sectionnumber, subcjetsemesterid, projectid) VALUES (1, 1, 8);
INSERT INTO public.sections( sectionnumber, subcjetsemesterid, projectid) VALUES (1, 1, 9);

INSERT INTO public.studentssections ( studentid, sectionid, rating, dateofentry) VALUES ( 1, 1, 5.0, CURRENT_DATE);
INSERT INTO public.studentssections ( studentid, sectionid, rating, dateofentry) VALUES ( 2, 2, 5.0, CURRENT_DATE);
INSERT INTO public.studentssections ( studentid, sectionid, rating, dateofentry) VALUES ( 3, 3, 5.0, CURRENT_DATE);
INSERT INTO public.studentssections ( studentid, sectionid, rating, dateofentry) VALUES ( 4, 4, 5.0, CURRENT_DATE);
INSERT INTO public.studentssections ( studentid, sectionid, rating, dateofentry) VALUES ( 5, 5, 5.0, CURRENT_DATE);
INSERT INTO public.studentssections ( studentid, sectionid, rating, dateofentry) VALUES ( 6, 6, 5.0, CURRENT_DATE);
INSERT INTO public.studentssections ( studentid, sectionid, rating, dateofentry) VALUES ( 7, 7, 5.0, CURRENT_DATE);
INSERT INTO public.studentssections ( studentid, sectionid, rating, dateofentry) VALUES ( 8, 8, 5.0, CURRENT_DATE);
INSERT INTO public.studentssections ( studentid, sectionid, rating, dateofentry) VALUES ( 9, 9, 5.0, CURRENT_DATE);

INSERT INTO public.meetings( studentssectionsid, dateofentry, rating) VALUES (1, CURRENT_DATE, 5.0);
INSERT INTO public.meetings( studentssectionsid, dateofentry, rating) VALUES (1, CURRENT_DATE, 5.0);
INSERT INTO public.meetings( studentssectionsid, dateofentry, rating) VALUES (1, CURRENT_DATE, 5.0);
INSERT INTO public.meetings( studentssectionsid, dateofentry, rating) VALUES (1, CURRENT_DATE, 5.0);
INSERT INTO public.meetings( studentssectionsid, dateofentry, rating) VALUES (1, CURRENT_DATE, 5.0);
INSERT INTO public.meetings( studentssectionsid, dateofentry, rating) VALUES (1, CURRENT_DATE, 5.0);
