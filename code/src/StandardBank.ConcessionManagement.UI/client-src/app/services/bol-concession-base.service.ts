import { FormGroup } from "@angular/forms";
import { ConditionType } from "../models/condition-type";
import { BolConcession } from "../models/bol-concession";
import { ConcessionRelationshipDetail } from "../models/concession-relationship-detail";



export class BolConcessionBaseService {

    showHide = false;
    errorMessage: String;
    isLoading = true;
    validationError: String[];
    saveMessage: String;
    warningMessage: String;
    canEdit = false;
    isRecalling = false;
    motivationEnabled = false;
    canBcmApprove = false;
    canPcmApprove = false;
    canApproveChanges = false;
    isEditing = false;
    public bolConcessionForm: FormGroup;
    selectedConditionTypes: ConditionType[];
    bolConcession: BolConcession;
    concessionReferenceId: string;

    constructor() { }

    disableFieldBase(fieldname: string, index: number = null, concessionRelationship: ConcessionRelationshipDetail = null) {
        switch (fieldname) {
            case 'compress':
                this.showHide = !this.showHide;
                break;
            case 'errorMessage':
                return (this.errorMessage) && !this.isLoading;
            case 'validationError':
                return (this.validationError) && !this.isLoading;
            case 'saveMessage':
                return this.saveMessage && !this.isLoading;
            case 'warningMessage':
                return this.warningMessage && !this.isLoading;
            case 'SMTDealNumber':
                return (this.isRecalling || this.canEdit) ? null : '';
            case 'motivationEnabled':
                return this.motivationEnabled ? null : '';
            case 'saveDisable':
                return this.saveMessage ? '' : null;
            case 'Comments':
                return this.canBcmApprove || this.canPcmApprove || this.canApproveChanges;
            case 'NewConcession':
                return this.canPcmApprove || this.isEditing || this.isRecalling;
            case 'viewConcessionTableCanEdit':
                return this.canEdit ? null : '';
            case 'concessionItemRowsDelete':
                return this.bolConcessionForm.controls.concessionItemRows.value.length > 1 && !this.saveMessage;
            case 'hasNoConditions':
                return this.bolConcessionForm.controls.conditionItemsRows.value.length == 0;
            case 'hasConditions':
                return this.bolConcessionForm.controls.conditionItemsRows.value.length > 0;
            case 'archiveDelete':
                return this.bolConcessionForm.controls.concessionItemRows.value.length > 1;
            case 'noCommentsAdded':
                return !this.bolConcession.concession.concessionComments || this.bolConcession.concession.concessionComments.length == 0;
            case 'noRelatedConcessions':
                return !this.bolConcession.concession.concessionRelationshipDetails || this.bolConcession.concession.concessionRelationshipDetails.length == 0;
            case 'ProductType':
                return this.selectedConditionTypes[index] != null;
            case 'InterestRateDisable':
                return this.selectedConditionTypes[index] != null && this.selectedConditionTypes[index].enableInterestRate ? null : '';
            case 'VolumeDisable':
                return this.selectedConditionTypes[index] != null && this.selectedConditionTypes[index].enableConditionVolume ? null : '';
            case 'ValueDisable':
                return this.selectedConditionTypes[index] != null && this.selectedConditionTypes[index].enableConditionValue ? null : '';
            case 'AddCondition':
                return this.canBcmApprove || this.canPcmApprove || this.isEditing || this.isRecalling;
            case 'parentReferenceCheck':
                return concessionRelationship.parentConcessionReference == this.concessionReferenceId;
            case 'childReferenceCheck':
                return concessionRelationship.childConcessionReference == this.concessionReferenceId;
            default:
                break;
        }
    }

}

