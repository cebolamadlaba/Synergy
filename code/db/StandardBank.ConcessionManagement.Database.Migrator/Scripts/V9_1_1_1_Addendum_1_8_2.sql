

Create Table rtblExtensionFee
(
	pkExtensionFeeId Int Not Null Identity(1,1) Primary Key,
	ExtensionFee Decimal(5,2) Not Null,
	IsActive Bit Not Null,
	DateEffectiveFrom DateTime Not Null
)