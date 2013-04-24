--
-- PostgreSQL database dump
--

-- Dumped from database version 9.1.3
-- Dumped by pg_dump version 9.1.3
-- Started on 2013-04-24 11:31:09

SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

--
-- TOC entry 177 (class 3079 OID 11639)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 1948 (class 0 OID 0)
-- Dependencies: 177
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

--
-- TOC entry 189 (class 1255 OID 26500)
-- Dependencies: 533 5
-- Name: years_of_service(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION years_of_service() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
declare months integer; 
years float; 
begin 
select cast((select sum(months_of_service) from experience where student=new.student) as float)/12 into years ;
update student set experience=years where id=new.student; 
return null; 
end; 
$$;


ALTER FUNCTION public.years_of_service() OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 164 (class 1259 OID 26252)
-- Dependencies: 5
-- Name: category; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE category (
    id integer NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE public.category OWNER TO postgres;

--
-- TOC entry 163 (class 1259 OID 26250)
-- Dependencies: 164 5
-- Name: category_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE category_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.category_id_seq OWNER TO postgres;

--
-- TOC entry 1949 (class 0 OID 0)
-- Dependencies: 163
-- Name: category_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE category_id_seq OWNED BY category.id;


--
-- TOC entry 1950 (class 0 OID 0)
-- Dependencies: 163
-- Name: category_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('category_id_seq', 6, true);


--
-- TOC entry 175 (class 1259 OID 26473)
-- Dependencies: 5
-- Name: experience; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE experience (
    id integer NOT NULL,
    student integer,
    company character varying(50) NOT NULL,
    months_of_service integer NOT NULL
);


ALTER TABLE public.experience OWNER TO postgres;

--
-- TOC entry 174 (class 1259 OID 26471)
-- Dependencies: 175 5
-- Name: experience_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE experience_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.experience_id_seq OWNER TO postgres;

--
-- TOC entry 1951 (class 0 OID 0)
-- Dependencies: 174
-- Name: experience_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE experience_id_seq OWNED BY experience.id;


--
-- TOC entry 1952 (class 0 OID 0)
-- Dependencies: 174
-- Name: experience_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('experience_id_seq', 6, true);


--
-- TOC entry 166 (class 1259 OID 26372)
-- Dependencies: 1896 1897 1898 1899 5
-- Name: foogle_user; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE foogle_user (
    id integer NOT NULL,
    email character varying(50) NOT NULL,
    firstname character varying(50),
    lastname character varying(50),
    title integer,
    role character(1) DEFAULT 'S'::bpchar,
    activity character(1) DEFAULT 'P'::bpchar,
    confirmed bit(1),
    CONSTRAINT foogle_user_activity_check CHECK ((activity = ANY (ARRAY['A'::bpchar, 'P'::bpchar]))),
    CONSTRAINT foogle_user_role_check CHECK ((role = ANY (ARRAY['P'::bpchar, 'S'::bpchar, 'A'::bpchar])))
);


ALTER TABLE public.foogle_user OWNER TO postgres;

--
-- TOC entry 165 (class 1259 OID 26370)
-- Dependencies: 5 166
-- Name: foogle_user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE foogle_user_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.foogle_user_id_seq OWNER TO postgres;

--
-- TOC entry 1953 (class 0 OID 0)
-- Dependencies: 165
-- Name: foogle_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE foogle_user_id_seq OWNED BY foogle_user.id;


--
-- TOC entry 1954 (class 0 OID 0)
-- Dependencies: 165
-- Name: foogle_user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('foogle_user_id_seq', 1, true);


--
-- TOC entry 176 (class 1259 OID 26495)
-- Dependencies: 5
-- Name: months; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE months (
    sum bigint
);


ALTER TABLE public.months OWNER TO postgres;

--
-- TOC entry 172 (class 1259 OID 26438)
-- Dependencies: 5
-- Name: recommendation; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE recommendation (
    skill integer NOT NULL,
    teacher integer NOT NULL,
    student integer NOT NULL,
    id integer NOT NULL
);


ALTER TABLE public.recommendation OWNER TO postgres;

--
-- TOC entry 171 (class 1259 OID 26436)
-- Dependencies: 5 172
-- Name: recommendation_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE recommendation_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.recommendation_id_seq OWNER TO postgres;

--
-- TOC entry 1955 (class 0 OID 0)
-- Dependencies: 171
-- Name: recommendation_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE recommendation_id_seq OWNED BY recommendation.id;


--
-- TOC entry 1956 (class 0 OID 0)
-- Dependencies: 171
-- Name: recommendation_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('recommendation_id_seq', 1, true);


--
-- TOC entry 170 (class 1259 OID 26425)
-- Dependencies: 5
-- Name: skill; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE skill (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    category integer NOT NULL
);


ALTER TABLE public.skill OWNER TO postgres;

--
-- TOC entry 169 (class 1259 OID 26423)
-- Dependencies: 5 170
-- Name: skill_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE skill_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.skill_id_seq OWNER TO postgres;

--
-- TOC entry 1957 (class 0 OID 0)
-- Dependencies: 169
-- Name: skill_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE skill_id_seq OWNED BY skill.id;


--
-- TOC entry 1958 (class 0 OID 0)
-- Dependencies: 169
-- Name: skill_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('skill_id_seq', 23, true);


--
-- TOC entry 173 (class 1259 OID 26459)
-- Dependencies: 5
-- Name: student; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE student (
    id integer NOT NULL,
    jmbag character varying(5) NOT NULL,
    gpa double precision NOT NULL,
    experience double precision
);


ALTER TABLE public.student OWNER TO postgres;

--
-- TOC entry 162 (class 1259 OID 26244)
-- Dependencies: 5
-- Name: title; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE title (
    id integer NOT NULL,
    title character varying(50) NOT NULL
);


ALTER TABLE public.title OWNER TO postgres;

--
-- TOC entry 161 (class 1259 OID 26242)
-- Dependencies: 162 5
-- Name: title_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE title_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.title_id_seq OWNER TO postgres;

--
-- TOC entry 1959 (class 0 OID 0)
-- Dependencies: 161
-- Name: title_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE title_id_seq OWNED BY title.id;


--
-- TOC entry 1960 (class 0 OID 0)
-- Dependencies: 161
-- Name: title_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('title_id_seq', 7, true);


--
-- TOC entry 168 (class 1259 OID 26391)
-- Dependencies: 5
-- Name: user_category; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE user_category (
    category integer NOT NULL,
    foogle_user integer NOT NULL,
    id integer NOT NULL
);


ALTER TABLE public.user_category OWNER TO postgres;

--
-- TOC entry 167 (class 1259 OID 26389)
-- Dependencies: 5 168
-- Name: user_category_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE user_category_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.user_category_id_seq OWNER TO postgres;

--
-- TOC entry 1961 (class 0 OID 0)
-- Dependencies: 167
-- Name: user_category_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE user_category_id_seq OWNED BY user_category.id;


--
-- TOC entry 1962 (class 0 OID 0)
-- Dependencies: 167
-- Name: user_category_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('user_category_id_seq', 1, true);


--
-- TOC entry 1894 (class 2604 OID 26255)
-- Dependencies: 164 163 164
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY category ALTER COLUMN id SET DEFAULT nextval('category_id_seq'::regclass);


--
-- TOC entry 1903 (class 2604 OID 26476)
-- Dependencies: 174 175 175
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY experience ALTER COLUMN id SET DEFAULT nextval('experience_id_seq'::regclass);


--
-- TOC entry 1895 (class 2604 OID 26375)
-- Dependencies: 166 165 166
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY foogle_user ALTER COLUMN id SET DEFAULT nextval('foogle_user_id_seq'::regclass);


--
-- TOC entry 1902 (class 2604 OID 26441)
-- Dependencies: 171 172 172
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY recommendation ALTER COLUMN id SET DEFAULT nextval('recommendation_id_seq'::regclass);


--
-- TOC entry 1901 (class 2604 OID 26428)
-- Dependencies: 170 169 170
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY skill ALTER COLUMN id SET DEFAULT nextval('skill_id_seq'::regclass);


--
-- TOC entry 1893 (class 2604 OID 26247)
-- Dependencies: 162 161 162
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY title ALTER COLUMN id SET DEFAULT nextval('title_id_seq'::regclass);


--
-- TOC entry 1900 (class 2604 OID 26394)
-- Dependencies: 168 167 168
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY user_category ALTER COLUMN id SET DEFAULT nextval('user_category_id_seq'::regclass);


--
-- TOC entry 1935 (class 0 OID 26252)
-- Dependencies: 164
-- Data for Name: category; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY category (id, name) FROM stdin;
1	Strategy and architecture
2	Business change
3	Solution development and implementation
4	Service menagement
5	Procurement and menagement support
6	Client interface
\.


--
-- TOC entry 1941 (class 0 OID 26473)
-- Dependencies: 175
-- Data for Name: experience; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY experience (id, student, company, months_of_service) FROM stdin;
4	1	gadfg	3
5	1	gadfg	7
6	1	gadfg	9
\.


--
-- TOC entry 1936 (class 0 OID 26372)
-- Dependencies: 166
-- Data for Name: foogle_user; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY foogle_user (id, email, firstname, lastname, title, role, activity, confirmed) FROM stdin;
1	ldetic@foi.hr	Ljiljana	Pintari†	\N	S	P	0
\.


--
-- TOC entry 1942 (class 0 OID 26495)
-- Dependencies: 176
-- Data for Name: months; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY months (sum) FROM stdin;
10
\.


--
-- TOC entry 1939 (class 0 OID 26438)
-- Dependencies: 172
-- Data for Name: recommendation; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY recommendation (skill, teacher, student, id) FROM stdin;
\.


--
-- TOC entry 1938 (class 0 OID 26425)
-- Dependencies: 170
-- Data for Name: skill; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY skill (id, name, category) FROM stdin;
1	IT governance	1
2	Information menagement	1
3	Information systems coordination	1
4	Information security	1
5	Information assurance	1
6	Information analysis	1
7	Information content publiching	1
8	Consultancy	1
9	Technical specialism	1
10	Research	1
11	Innovation	1
12	Business process improvement	1
13	Enterprise & business architecture development	1
14	Business risk menagement	1
15	Sustainability strategy	1
16	Emerging technology monitoring	1
17	Continuity menagement	1
18	Software development process improvement	1
19	Sustainability menagement for IT	1
20	Network planning	1
21	Solution architecture	1
22	Data menagement	1
23	Methods and tools	1
\.


--
-- TOC entry 1940 (class 0 OID 26459)
-- Dependencies: 173
-- Data for Name: student; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY student (id, jmbag, gpa, experience) FROM stdin;
1	39290	4.2999999999999998	1.5833333333333333
\.


--
-- TOC entry 1934 (class 0 OID 26244)
-- Dependencies: 162
-- Data for Name: title; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY title (id, title) FROM stdin;
1	Pristupnik
2	Baccalareus
3	Magistar
4	Sveuźiliçni specijalist
5	Doktor
6	Docent
7	Profesor
\.


--
-- TOC entry 1937 (class 0 OID 26391)
-- Dependencies: 168
-- Data for Name: user_category; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY user_category (category, foogle_user, id) FROM stdin;
1	1	1
\.


--
-- TOC entry 1907 (class 2606 OID 26257)
-- Dependencies: 164 164
-- Name: category_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY category
    ADD CONSTRAINT category_pkey PRIMARY KEY (id);


--
-- TOC entry 1923 (class 2606 OID 26478)
-- Dependencies: 175 175
-- Name: experience_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY experience
    ADD CONSTRAINT experience_pkey PRIMARY KEY (id);


--
-- TOC entry 1909 (class 2606 OID 26383)
-- Dependencies: 166 166
-- Name: foogle_user_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY foogle_user
    ADD CONSTRAINT foogle_user_email_key UNIQUE (email);


--
-- TOC entry 1911 (class 2606 OID 26381)
-- Dependencies: 166 166
-- Name: foogle_user_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY foogle_user
    ADD CONSTRAINT foogle_user_pkey PRIMARY KEY (id);


--
-- TOC entry 1917 (class 2606 OID 26443)
-- Dependencies: 172 172 172 172
-- Name: recommendation_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY recommendation
    ADD CONSTRAINT recommendation_pkey PRIMARY KEY (skill, teacher, student);


--
-- TOC entry 1915 (class 2606 OID 26430)
-- Dependencies: 170 170
-- Name: skill_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY skill
    ADD CONSTRAINT skill_pkey PRIMARY KEY (id);


--
-- TOC entry 1919 (class 2606 OID 26465)
-- Dependencies: 173 173
-- Name: student_jmbag_key; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY student
    ADD CONSTRAINT student_jmbag_key UNIQUE (jmbag);


--
-- TOC entry 1921 (class 2606 OID 26463)
-- Dependencies: 173 173
-- Name: student_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY student
    ADD CONSTRAINT student_pkey PRIMARY KEY (id);


--
-- TOC entry 1905 (class 2606 OID 26249)
-- Dependencies: 162 162
-- Name: title_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY title
    ADD CONSTRAINT title_pkey PRIMARY KEY (id);


--
-- TOC entry 1913 (class 2606 OID 26396)
-- Dependencies: 168 168 168
-- Name: user_category_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY user_category
    ADD CONSTRAINT user_category_pkey PRIMARY KEY (category, foogle_user);


--
-- TOC entry 1933 (class 2620 OID 26501)
-- Dependencies: 175 189
-- Name: years_of_service_insert; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER years_of_service_insert AFTER INSERT ON experience FOR EACH ROW EXECUTE PROCEDURE years_of_service();


--
-- TOC entry 1932 (class 2606 OID 26479)
-- Dependencies: 1920 173 175
-- Name: experience_student_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY experience
    ADD CONSTRAINT experience_student_fkey FOREIGN KEY (student) REFERENCES student(id) ON UPDATE CASCADE ON DELETE RESTRICT;


--
-- TOC entry 1924 (class 2606 OID 26384)
-- Dependencies: 166 162 1904
-- Name: foogle_user_title_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY foogle_user
    ADD CONSTRAINT foogle_user_title_fkey FOREIGN KEY (title) REFERENCES title(id) ON UPDATE CASCADE ON DELETE RESTRICT;


--
-- TOC entry 1928 (class 2606 OID 26444)
-- Dependencies: 170 1914 172
-- Name: recommendation_skill_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY recommendation
    ADD CONSTRAINT recommendation_skill_fkey FOREIGN KEY (skill) REFERENCES skill(id);


--
-- TOC entry 1930 (class 2606 OID 26454)
-- Dependencies: 1910 166 172
-- Name: recommendation_student_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY recommendation
    ADD CONSTRAINT recommendation_student_fkey FOREIGN KEY (student) REFERENCES foogle_user(id) ON UPDATE CASCADE ON DELETE RESTRICT;


--
-- TOC entry 1929 (class 2606 OID 26449)
-- Dependencies: 166 1910 172
-- Name: recommendation_teacher_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY recommendation
    ADD CONSTRAINT recommendation_teacher_fkey FOREIGN KEY (teacher) REFERENCES foogle_user(id) ON UPDATE CASCADE ON DELETE RESTRICT;


--
-- TOC entry 1927 (class 2606 OID 26431)
-- Dependencies: 1906 164 170
-- Name: skill_category_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY skill
    ADD CONSTRAINT skill_category_fkey FOREIGN KEY (category) REFERENCES category(id) ON UPDATE CASCADE ON DELETE RESTRICT;


--
-- TOC entry 1931 (class 2606 OID 26466)
-- Dependencies: 166 173 1910
-- Name: student_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY student
    ADD CONSTRAINT student_id_fkey FOREIGN KEY (id) REFERENCES foogle_user(id) ON UPDATE CASCADE ON DELETE RESTRICT;


--
-- TOC entry 1925 (class 2606 OID 26397)
-- Dependencies: 1906 168 164
-- Name: user_category_category_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY user_category
    ADD CONSTRAINT user_category_category_fkey FOREIGN KEY (category) REFERENCES category(id) ON UPDATE CASCADE ON DELETE RESTRICT;


--
-- TOC entry 1926 (class 2606 OID 26402)
-- Dependencies: 1910 166 168
-- Name: user_category_foogle_user_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY user_category
    ADD CONSTRAINT user_category_foogle_user_fkey FOREIGN KEY (foogle_user) REFERENCES foogle_user(id) ON UPDATE CASCADE ON DELETE RESTRICT;


--
-- TOC entry 1947 (class 0 OID 0)
-- Dependencies: 5
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2013-04-24 11:31:09

--
-- PostgreSQL database dump complete
--
