CREATE TABLE rtblSubRole (
    subRoleId int IDENTITY(1,1) NOT NULL  PRIMARY KEY,
    name varchar(255) NOT NULL,
    Active bit default 1
);

INSERT INTO rtblSubRole(name)
VALUES ('BOLConsultant')
        ,('TradeBanker');

ALTER TABLE tblUserRole
ADD  fkSubRoleId int  FOREIGN KEY (fkSubRoleId) REFERENCES rtblSubRole(subRoleId) 
