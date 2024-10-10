# GerenciamentoAssuntos

Serviço para realizar buscar de assuntos.

## Índice

- [Instalação](#instalacao)
- [Uso](#uso)
- [Contribuindo](#contribuindo)
- [Licença](#licenca)

## Instalação

Ao executar a aplicacao atraves do Dcoker Compose , devemos acessar o PgAdmin atraves da porta http://localhost:15432/

Logo após devemos executar o script para criarmos a nossa base e tabelas:

CREATE DATABASE "GerenciamentoAssuntos"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
	
	CREATE TABLE AssuntoStatus (
    id SERIAL PRIMARY KEY,
	nome  VARCHAR(100)
   );
  
	
   CREATE TABLE Assunto (
    id SERIAL PRIMARY KEY,
	titulo  VARCHAR(100),
    palavrasChaves VARCHAR(100),
    status INT REFERENCES AssuntoStatus(id),
	dataCriacao Date,
	dataAtualizacao Date
 );
 
    CREATE TABLE Conteudo (
    id SERIAL PRIMARY KEY,
	Link  VARCHAR(300),
    AssuntoId INT REFERENCES Assunto(id)
   );


    INSERT INTO assuntoStatus (nome) VALUES ('Pendente');
    INSERT INTO assuntoStatus (nome) VALUES ('EmProgresso');
    INSERT INTO assuntoStatus (nome) VALUES ('Concluído');


# Para realizarmos a pesquisa.
Nos devemos cadastrar primeiro o assunto, atraves do endpoint - POST(Assuntos/assuntos).
E para buscarmos as informações dos links dos assuntos - GET(Conteudo/GetByConteudo) - utilizando o ID do assuno como parametro.


