import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { Province } from "../../models/province";
import { ProvinceService } from "../../services/province.service";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Location } from '@angular/common';

@Component({
    selector: 'app-province',
    templateUrl: './province.component.html',
    styleUrls: ['./province.component.css']
})
export class ProvinceComponent implements OnInit {
    //public provinceForm: FormGroup;
    private sub: any;
    observableProvince: Observable<Province[]>;
    validationError: String[];
    errorMessage: String;
    isLoading = false;
    myEditProvince: Province;
    submitState: String;
    provinces: Province[];

    constructor( @Inject(ProvinceService)
    private provinceService,
        private location: Location,
        private formBuilder: FormBuilder) {
        this.provinces = [new Province()];
        this.myEditProvince = new Province();
    }

    ngOnInit() {
        this.observableProvince = this.provinceService.getProvinces();
        this.observableProvince.subscribe(results => { this.provinces = results; }, error => {
            this.errorMessage = <any>error;
            this.isLoading = false;
        });
        this.submitState = 'Create';
    }

    provinceView(province) {
        this.submitState = 'Update';
        this.myEditProvince = province;
    }

    onSubmit() {
        if (!this.validationError) {

            if (this.myEditProvince.id == undefined) {
                this.myEditProvince.isActive = true;
            }
            this.provinceService.postProvince(this.myEditProvince).subscribe(entity => {
                if (this.myEditProvince.id == undefined) {
                    this.provinces.push(this.myEditProvince);
                }
                this.onCancel();
                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    onCancel() {
        this.submitState = 'Create';
        this.myEditProvince = new Province();
    }

    goBack() {
        this.location.back();
    }
}
