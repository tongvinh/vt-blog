import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { AddPostSeriesRequest, AdminApiPostApiClient, PostDto, SeriesInListDto } from 'src/app/api/admin-api.service.generated';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { AdminApiSeriesApiClient } from '../../../api/admin-api.service.generated';
import { AlertService } from 'src/app/shared/services/alert.service';
import { MessageConstants } from 'src/app/shared/constants/messages.constant';
import { HighlightSpanKind } from 'typescript/lib/tsserverlibrary';

@Component({
  selector: 'app-post-series',
  templateUrl: './post-series.component.html',
})
export class PostSeriesComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public allSeries: any[] = [];
  public postSeries: any[]
  public selectedEntity: PostDto;
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private postApiClient: AdminApiPostApiClient,
    private seriesApiClient: AdminApiSeriesApiClient,
    private alertService: AlertService
  ) { }
  ngOnInit(): void {
    //Init form
    this.builForm();
    //Load data to form
    var series = this.seriesApiClient.getAllSeries();
    this.toggleBlockUI(true);
    forkJoin({
      series
    })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: any) => {
          //Push categories to dropdown list
          var series = response.series as SeriesInListDto[];
          series.forEach(element => {
            this.allSeries.push({
              value: element.id,
              label: element.name
            });
          });

          if (this.utilService.isEmpty(this.config.data?.id) == false) {
            this.loadSeries(this.config.data?.id);
          } else {
            this.toggleBlockUI(false);
          }
        },
        error: () =>{
          this.toggleBlockUI(false);
        }
      });
  }
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  // Validate
  noSpecial: RegExp = /^[^<>*!_~]+$/;
  validationMessages = {
    seriesId: [{ type: 'required', message: 'Bạn phải chọn loạt bài' }],
    sortOrder: [{ type: 'required', message: 'Bạn phải nhập thứ tự' }],
  };

  loadSeries(id: string) {
    this.postApiClient
      .getSeriesBelong(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: SeriesInListDto[]) => {
          this.postSeries = response;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        }
      })
  }

  removeSeries(id: string) {
    var body: AddPostSeriesRequest = new AddPostSeriesRequest({
      postId: this.config.data.id,
      seriesId: id
    });
    this.seriesApiClient
      .deletePostSeries(body)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.alertService.showSuccess(MessageConstants.DELETED_OK_MSG);
          this.loadSeries(this.config.data?.id);
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        }
      });
  }

  saveChange() {
    this.toggleBlockUI(true);
    this.saveData();
  }

  private saveData() {
    this.toggleBlockUI(true);
    var body: AddPostSeriesRequest = new AddPostSeriesRequest({
      postId: this.config.data.id,
      seriesId: this.form.controls['seriesId'].value,
      sortOrder: this.form.controls['sortOrder'].value
    });
    this.seriesApiClient
      .addPostSeries(body)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.alertService.showSuccess('Đã thêm bài viết thành công');
          this.loadSeries(this.config.data?.id);
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        }
      });
  }

  builForm() {
    this.form = this.fb.group({
      seriesId: new FormControl(null, Validators.required),
      sortOrder: new FormControl(0, Validators.required),
    });
  }
  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.btnDisabled = true;
      this.blockedPanelDetail = true;
    } else {
      setTimeout(() => {
        this.btnDisabled = false;
        this.blockedPanelDetail = false;
      }, 1000);
    }
  }

}
