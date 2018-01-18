import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { RegionService } from '../../services/region.service';
import { Observable } from "rxjs";
import { Region } from '../../models/region';

@Component({
    selector: 'app-region',
    templateUrl: './region.component.html',
    styleUrls: ['./region.component.css']
})
export class RegionComponent implements OnInit {
    errorMessage: string;
    validationError: string[];
    saveMessage: string;
    isLoading = true;

    observableRegions: Observable<Region[]>;
    regions: Region[];
    addRegion: Region;

    observableErrors: Observable<string[]>;
    observableSave: Observable<boolean>;

    constructor(private location: Location, private regionService: RegionService) {
        this.addRegion = new Region();
    }

    ngOnInit() {
        this.loadRegions();
    }

    loadRegions() {
        this.isLoading = true;

        this.observableRegions = this.regionService.getAll();
        this.observableRegions.subscribe(regions => {
            this.regions = regions;
            this.isLoading = false;
        }, error => {
            this.isLoading = false;
            this.errorMessage = <any>error;
        });
    }

    createRegion() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        this.addRegion.isActive = true;

        this.observableErrors = this.regionService.validate(this.addRegion);
        this.observableErrors.subscribe(errors => {
            if (errors != null && errors.length > 0) {
                this.validationError = errors;
                this.isLoading = false;
            } else {
                this.observableSave = this.regionService.create(this.addRegion);
                this.observableSave.subscribe(errors => {
                    this.saveMessage = "Region created successfully!";
                    this.addRegion = new Region();

                    //after saving reload the regions
                    this.loadRegions();
                }, error => {
                    this.isLoading = false;
                    this.errorMessage = <any>error;
                });
            }
        }, error => {
            this.isLoading = false;
            this.errorMessage = <any>error;
        });
    }

    goBack() {
        this.location.back();
    }
}
