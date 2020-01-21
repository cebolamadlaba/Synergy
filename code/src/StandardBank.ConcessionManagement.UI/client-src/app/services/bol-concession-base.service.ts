


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

    constructor() { }

    disableFieldBase(fieldname: string) {
        switch (fieldname) {
            case 'compress':
                this.showHide = !this.showHide;
                break;
            case 'errorMessage':
                return (this.errorMessage) && !this.isLoading;
            case 'validationError':
                return (this.validationError) && !this.isLoading;
            case 'saveMessage':
                return this.saveMessage && !this.isLoading
            case 'warningMessage':
                return this.warningMessage && !this.isLoading
            case 'SMTDealNumber':
                return (this.isRecalling || this.canEdit) ? null : ''
            case 'motivationEnabled':
                return this.motivationEnabled ? null : ''
            case 'addSMTDealNumber':
                return this.saveMessage ? '' : null
            case 'addMotivationEnabled':
                return this.motivationEnabled ? null : ''
            case 'Comments':
                return this.canBcmApprove || this.canPcmApprove || this.canApproveChanges
            default:
                break;
        }
    }

}

