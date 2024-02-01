import { Component, OnDestroy, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { AdminApiPostApiClient, PostActivityLogDto } from 'src/app/api/admin-api.service.generated';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  selector: 'app-post-activity-logs',
  templateUrl: './post-activity-logs.component.html',
})
export class PostActivityLogsComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public items: any[] = [];
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private utilService: UtilityService,
    private postApiClient: AdminApiPostApiClient,
  ) { }

  ngOnInit(): void {
    //Load data to form
    this.toggleBlockUI(true);
    this.postApiClient.getActivityLogs(this.config.data.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PostActivityLogDto[]) => {
          this.items = response;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        }
      })
  }
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
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
