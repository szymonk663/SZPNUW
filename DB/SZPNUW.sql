CREATE DATABASE "SZPNUW"
	WITH ENCODING = 'WIN1250'
	TABLESPACE = pg_default;
	


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
    CONSTRAINT ux_projects UNIQUE (lecturerid, subjectid),
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
    projectid INTEGER NOT NULL,
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