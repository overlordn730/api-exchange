-- ============================================================
-- Api.Exchange - Script completo de base de datos Oracle
-- Ejecutar conectado como SYSDBA en el PDB ORCLPDB
-- ============================================================

-- ────────────────────────────────────────
-- Tablespace
-- ────────────────────────────────────────
CREATE TABLESPACE FINANCES_TS
    DATAFILE 'D:\ORACLE19APP\ORADATA\ORA19C\ORCLPDB\FINANCES01.DBF'
    SIZE 100M
    AUTOEXTEND ON NEXT 50M
    MAXSIZE 1G;

-- ────────────────────────────────────────
-- Usuario dueño del esquema
-- ────────────────────────────────────────
CREATE USER FINANCES IDENTIFIED BY "a.123456"
    DEFAULT TABLESPACE FINANCES_TS
    TEMPORARY TABLESPACE TEMP
    QUOTA UNLIMITED ON FINANCES_TS;

GRANT CONNECT, RESOURCE TO FINANCES;
GRANT CREATE SESSION TO FINANCES;
GRANT CREATE TABLE TO FINANCES;
GRANT CREATE SEQUENCE TO FINANCES;
GRANT CREATE VIEW TO FINANCES;

-- ────────────────────────────────────────
-- Usuario de la API
-- ────────────────────────────────────────
CREATE USER API_EXCHANGE IDENTIFIED BY "a.54321"
    DEFAULT TABLESPACE FINANCES_TS
    TEMPORARY TABLESPACE TEMP
    QUOTA UNLIMITED ON FINANCES_TS;

GRANT CONNECT TO API_EXCHANGE;
GRANT CREATE SESSION TO API_EXCHANGE;

-- ────────────────────────────────────────
-- Tabla: USERS
-- ────────────────────────────────────────
CREATE TABLE FINANCES.USERS (
    ID             NUMBER          GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    NAME           VARCHAR2(100)   NOT NULL,
    EMAIL          VARCHAR2(200)   NOT NULL,
    PASSWORD_HASH  VARCHAR2(255)   NOT NULL,
    IS_ACTIVE      NUMBER(1)       DEFAULT 1 NOT NULL,
    CONSTRAINT UQ_USERS_EMAIL UNIQUE (EMAIL),
    CONSTRAINT CHK_USERS_IS_ACTIVE CHECK (IS_ACTIVE IN (0, 1))
);

-- ────────────────────────────────────────
-- Tabla: ADDRESSES
-- ────────────────────────────────────────
CREATE TABLE FINANCES.ADDRESSES (
    ID       NUMBER          GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    USER_ID  NUMBER          NOT NULL,
    STREET   VARCHAR2(200)   NOT NULL,
    CITY     VARCHAR2(100)   NOT NULL,
    COUNTRY  VARCHAR2(100)   NOT NULL,
    ZIP_CODE VARCHAR2(20),
    CONSTRAINT FK_ADDRESSES_USER FOREIGN KEY (USER_ID)
        REFERENCES FINANCES.USERS(ID) ON DELETE CASCADE
);

CREATE INDEX FINANCES.IDX_ADDRESSES_USER_ID ON FINANCES.ADDRESSES(USER_ID);

-- ────────────────────────────────────────
-- Tabla: CURRENCIES
-- ────────────────────────────────────────
CREATE TABLE FINANCES.CURRENCIES (
    ID            NUMBER          GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    CODE          VARCHAR2(10)    NOT NULL,
    NAME          VARCHAR2(100)   NOT NULL,
    RATE_TO_BASE  NUMBER(18, 6)   NOT NULL,
    CONSTRAINT UQ_CURRENCIES_CODE UNIQUE (CODE),
    CONSTRAINT CHK_CURRENCIES_RATE CHECK (RATE_TO_BASE > 0)
);

-- ────────────────────────────────────────
-- Permisos de API_EXCHANGE sobre las tablas
-- ────────────────────────────────────────
GRANT SELECT, INSERT, UPDATE, DELETE ON FINANCES.USERS TO API_EXCHANGE;
GRANT SELECT, INSERT, UPDATE, DELETE ON FINANCES.ADDRESSES TO API_EXCHANGE;
GRANT SELECT, INSERT, UPDATE, DELETE ON FINANCES.CURRENCIES TO API_EXCHANGE;

-- ────────────────────────────────────────
-- Datos iniciales de monedas
-- PYG es la moneda base: RateToBase = 1
-- ────────────────────────────────────────
INSERT INTO FINANCES.CURRENCIES (CODE, NAME, RATE_TO_BASE) VALUES ('PYG', 'Guaraní Paraguayo', 1);
INSERT INTO FINANCES.CURRENCIES (CODE, NAME, RATE_TO_BASE) VALUES ('USD', 'Dólar Estadounidense', 7300);
INSERT INTO FINANCES.CURRENCIES (CODE, NAME, RATE_TO_BASE) VALUES ('EUR', 'Euro', 7900);
INSERT INTO FINANCES.CURRENCIES (CODE, NAME, RATE_TO_BASE) VALUES ('BRL', 'Real Brasileño', 1350);
INSERT INTO FINANCES.CURRENCIES (CODE, NAME, RATE_TO_BASE) VALUES ('ARS', 'Peso Argentino', 7.5);

COMMIT;