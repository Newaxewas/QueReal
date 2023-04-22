import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestCreatePageComponent } from './quest-create-page.component';

describe('QuestCreatePageComponent', () => {
  let component: QuestCreatePageComponent;
  let fixture: ComponentFixture<QuestCreatePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestCreatePageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuestCreatePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
