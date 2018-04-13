ALTER TABLE rtblTableNumber
ALTER COLUMN BaseRate decimal (18,2);

ALTER TABLE rtblTableNumber
ALTER column AdValorem  decimal (18,3);


ALTER TABLE rtblTransactionTableNumber
ALTER COLUMN Fee decimal (18,2)

ALTER TABLE rtblTransactionTableNumber
ALTER COLUMN AdValorem decimal (18,3)

ALTER TABLE tblConcessionCash
ALTER COLUMN AdValorem  decimal (18,3)

ALTER TABLE tblConcessionCash
ALTER COLUMN BaseRate  decimal (18,2)

ALTER TABLE tblConcessionTransactional
ALTER COLUMN Fee decimal (18,2)

ALTER TABLE tblConcessionTransactional
ALTER COLUMN AdValorem decimal (18,3)

