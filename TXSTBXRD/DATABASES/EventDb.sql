--
-- PostgreSQL database dump
--

-- Dumped from database version 14.5
-- Dumped by pg_dump version 14.4

-- Started on 2022-09-07 12:13:17

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 3310 (class 1262 OID 16394)
-- Name: EventDB; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "EventDB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Russian_Russia.1251';


ALTER DATABASE "EventDB" OWNER TO postgres;

\connect "EventDB"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 209 (class 1259 OID 16403)
-- Name: event; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.event (
    event_id integer NOT NULL,
    customer character varying(255)[] NOT NULL,
    event_type integer NOT NULL,
    description character varying(255)[],
    event_date timestamp without time zone NOT NULL
);


ALTER TABLE public.event OWNER TO postgres;

--
-- TOC entry 3304 (class 0 OID 16403)
-- Dependencies: 209
-- Data for Name: event; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.event (event_id, customer, event_type, description, event_date) FROM stdin;
\.


--
-- TOC entry 3164 (class 2606 OID 16409)
-- Name: event event_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.event
    ADD CONSTRAINT event_pkey PRIMARY KEY (event_id);


-- Completed on 2022-09-07 12:13:17

--
-- PostgreSQL database dump complete
--

