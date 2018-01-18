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
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    isLoading = true;

    observableRegions: Observable<Region[]>;
    regions: Region[];

    constructor(private location: Location, private regionService: RegionService) { }

    ngOnInit() {
        this.observableRegions = this.regionService.getAll();
        this.observableRegions.subscribe(regions => {
            this.regions = regions;
            this.isLoading = false;
        }, error => {
            this.isLoading = false;
            this.errorMessage = <any>error;
        });
    }

    goBack() {
        this.location.back();
    }
}
