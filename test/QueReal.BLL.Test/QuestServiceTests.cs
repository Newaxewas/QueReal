using System.Linq.Expressions;
using AutoMapper;
using QueReal.BLL.DTO.Quest;
using QueReal.BLL.Exceptions;
using QueReal.BLL.Interfaces;
using QueReal.BLL.Services;
using QueReal.DAL.Interfaces;
using QueReal.DAL.Models;

namespace QueReal.BLL.Test
{
    [TestFixture]
    public class QuestServiceTests
    {
        private const byte targetProgress = 81;

        private static readonly Guid questId = Guid.NewGuid();
        private static readonly Guid currentUserId = Guid.NewGuid();
        private static readonly Guid otherUserId = Guid.NewGuid();

        private static readonly Guid[] questItemIds
            = Enumerable.Range(0, 2)
                .Select(x => Guid.NewGuid())
                .ToArray();

        private static readonly string[] newQuestItemTitles
            = Enumerable.Range(0, 2)
                .Select(x => $"new title {x}")
                .ToArray();

        private Quest quest;
        private QuestItem questItem;
        private QuestCreateDto questCreateDto;
        private QuestEditDto questEditDto;

        private Mock<IRepository<Quest>> questRepositoryMock;
        private Mock<IRepository<QuestItem>> questItemRepositoryMock;
        private Mock<ICurrentUserService> currentUserServiceMock;
        private Mock<IMapper> mapperMock;

        private QuestService questService;

        [SetUp]
        public void SetUp()
        {
            quest = CreateQuest();
            questItem = CreateQuestItem();
            questCreateDto = CreateQuestCreateDto();
            questEditDto = CreateQuestEditDto();

            questRepositoryMock = new();
            questItemRepositoryMock = new();
            currentUserServiceMock = new();
            mapperMock = new();

            questService = new(
                questRepositoryMock.Object,
                questItemRepositoryMock.Object,
                currentUserServiceMock.Object,
                mapperMock.Object);

            SetupMappings();
            SetCurrentUserId();
        }

        [Test]
        public async Task CreateAsync_AddQuestToRepository()
        {
            await questService.CreateAsync(questCreateDto);

            questRepositoryMock.Verify(x => x.CreateAsync(quest), Times.Once);
        }

        [Test]
        public async Task CreateAsync_SetCreatorId()
        {
            await questService.CreateAsync(questCreateDto);

            quest.CreatorId.Should().Be(currentUserId);
        }

        [Test]
        public async Task GetAsync_WhenQuestNotFound_ThrowNotFoundException()
        {
            SetQuestCanBeFound(false);
            SetUserHasAccessToQuest(true);

            await questService.Invoking(x => x.GetAsync(questId))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task GetAsync_WhenUserDoesntHaveAccessToQuest_ThrowAccessDeniedException()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(false);

            await questService.Invoking(x => x.GetAsync(questId))
                .Should().ThrowAsync<AccessDeniedException>();
        }

        [Test]
        public async Task GetAsync_WhenValidationsPassed_ReturnQuest()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);

            var result = await questService.GetAsync(questId);

            result.Should().Be(quest);
        }

        [TestCase(1, 10, 0)]
        [TestCase(1, 20, 0)]
        [TestCase(2, 10, 10)]
        [TestCase(5, 10, 40)]
        [TestCase(5, 20, 80)]
        public async Task GetAllAsync_CallRepositoryMethodWithCorrectSkipCount(
            int pageNumber,
            int pageSize,
            int expectedSkipCount)
        {
            await questService.GetAllAsync(pageNumber, pageSize);

            questRepositoryMock.Verify(
                x => x.GetAllAsync(
                    It.IsAny<Expression<Func<Quest, bool>>>(),
                    It.IsAny<Func<IQueryable<Quest>, IOrderedQueryable<Quest>>>(),
                    expectedSkipCount,
                    pageSize),
                Times.Once);
        }

        [Test]
        public async Task EditAsync_WhenQuestNotFound_ThrowNotFoundException()
        {
            SetQuestCanBeFound(false);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);

            await questService.Invoking(x => x.EditAsync(questEditDto))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task EditAsync_WhenUserDoesntHaveAccessToQuest_ThrowAccessDeniedException()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(false);
            SetQuestCompletionNotApproved(true);

            await questService.Invoking(x => x.EditAsync(questEditDto))
                .Should().ThrowAsync<AccessDeniedException>();
        }

        [Test]
        public async Task EditAsync_WhenQuestCompletionApproved_ThrowBadRequestException()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(false);

            await questService.Invoking(x => x.EditAsync(questEditDto))
                .Should().ThrowAsync<BadRequestException>();
        }

        [Test]
        public async Task EditAsync_WhenValidationsPassed_SetNewTitle()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);

            await questService.EditAsync(questEditDto);

            quest.Title.Should().Be(questEditDto.Title);
        }

        [Test]
        public async Task EditAsync_WhenValidationsPassed_SetNewQuestItems()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);

            await questService.EditAsync(questEditDto);

            quest.QuestItems.Should()
                .HaveCount(2)
                .And.Contain(
                    x => x.Id == questItemIds[0]
                        && x.Title == newQuestItemTitles[0])
                .And.Contain(
                    x => x.Title == newQuestItemTitles[1]);
        }

        [Test]
        public async Task EditAsync_WhenValidationsPassed_UpdateQuest()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);

            await questService.EditAsync(questEditDto);

            questRepositoryMock.Verify(x => x.UpdateAsync(quest), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_WhenQuestNotFound_ThrowNotFoundException()
        {
            SetQuestCanBeFound(false);
            SetUserHasAccessToQuest(true);

            await questService.Invoking(x => x.DeleteAsync(questId))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task DeleteAsync_WhenUserDoesntHaveAccessToQuest_ThrowAccessDeniedException()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(false);

            await questService.Invoking(x => x.DeleteAsync(questId))
                .Should().ThrowAsync<AccessDeniedException>();
        }

        [Test]
        public async Task DeleteAsync_WhenValidationsPassed_DeleteQuest()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);

            await questService.DeleteAsync(questId);

            questRepositoryMock.Verify(x => x.DeleteAsync(quest), Times.Once);
        }

        [Test]
        public async Task SetProgressAsync_WhenQuestItemNotFound_ThrowNotFoundException()
        {
            SetQuestItemCanBeFound(false);
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);

            await questService.Invoking(x => x.SetProgressAsync(questItem.Id, targetProgress))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task SetProgressAsync_WhenQuestNotFound_ThrowNotFoundException()
        {
            SetQuestItemCanBeFound(true);
            SetQuestCanBeFound(false);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);

            await questService.Invoking(x => x.SetProgressAsync(questItem.Id, targetProgress))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task SetProgressAsync_WhenUserDoesntHaveAccessToQuest_ThrowAccessDeniedException()
        {
            SetQuestItemCanBeFound(true);
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(false);
            SetQuestCompletionNotApproved(true);

            await questService.Invoking(x => x.SetProgressAsync(questItem.Id, targetProgress))
                .Should().ThrowAsync<AccessDeniedException>();
        }

        [Test]
        public async Task SetProgressAsync_WhenQuestCompletionApproved_ThrowBadRequestException()
        {
            SetQuestItemCanBeFound(true);
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(false);

            await questService.Invoking(x => x.SetProgressAsync(questItem.Id, targetProgress))
                .Should().ThrowAsync<BadRequestException>();
        }

        [Test]
        public async Task SetProgressAsync_WhenValidationsPassed_SetNewProgress()
        {
            questItem.Progress = 31;

            SetQuestItemCanBeFound(true);
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);

            await questService.SetProgressAsync(questItem.Id, targetProgress);

            questItem.Progress.Should().Be(targetProgress);
        }

        [Test]
        public async Task SetProgressAsync_WhenValidationsPassed_UpdateQuestItem()
        {
            SetQuestItemCanBeFound(true);
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);

            await questService.SetProgressAsync(questItem.Id, targetProgress);

            questItemRepositoryMock.Verify(x => x.UpdateAsync(questItem), Times.Once);
        }

        [Test]
        public async Task SetProgressAsync_WhenValidationsPassed_SetUpdateTime()
        {
            SetQuestItemCanBeFound(true);
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);

            await questService.SetProgressAsync(questItem.Id, targetProgress);

            quest.UpdateTime.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }

        [Test]
        public async Task SetProgressAsync_WhenValidationsPassed_UpdateQuest()
        {
            SetQuestItemCanBeFound(true);
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);

            await questService.SetProgressAsync(questItem.Id, targetProgress);

            questRepositoryMock.Verify(x => x.UpdateAsync(quest), Times.Once);
        }

        [Test]
        public async Task ApproveCompletionAsync_WhenQuestNotFound_ThrowNotFoundException()
        {
            SetQuestCanBeFound(false);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);
            SetAllQuestItemsHaveFullProgress(true);

            await questService.Invoking(x => x.ApproveCompletionAsync(questId))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ApproveCompletionAsync_WhenUserDoesntHaveAccessToQuest_ThrowAccessDeniedException()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(false);
            SetQuestCompletionNotApproved(true);
            SetAllQuestItemsHaveFullProgress(true);

            await questService.Invoking(x => x.ApproveCompletionAsync(questId))
                .Should().ThrowAsync<AccessDeniedException>();
        }

        [Test]
        public async Task ApproveCompletionAsync_WhenQuestCompletionApproved_ThrowBadRequestException()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(false);
            SetAllQuestItemsHaveFullProgress(true);

            await questService.Invoking(x => x.ApproveCompletionAsync(questId))
                .Should().ThrowAsync<BadRequestException>();
        }

        [Test]
        public async Task ApproveCompletionAsync_WhenNotAllQuestItemsHaveFullProgress_ThrowBadRequestException()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);
            SetAllQuestItemsHaveFullProgress(false);

            await questService.Invoking(x => x.ApproveCompletionAsync(questId))
                .Should().ThrowAsync<BadRequestException>();
        }

        [Test]
        public async Task ApproveCompletionAsync_WhenValidationsPassed_SetApprovedTime()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);
            SetAllQuestItemsHaveFullProgress(true);

            await questService.ApproveCompletionAsync(questId);

            quest.ApprovedTime.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }

        [Test]
        public async Task ApproveCompletionAsync_WhenValidationsPassed_UpdateQuest()
        {
            SetQuestCanBeFound(true);
            SetUserHasAccessToQuest(true);
            SetQuestCompletionNotApproved(true);
            SetAllQuestItemsHaveFullProgress(true);

            await questService.ApproveCompletionAsync(questId);

            questRepositoryMock.Verify(x => x.UpdateAsync(quest), Times.Once);
        }

        [TestCase(1, 10, 0)]
        [TestCase(1, 20, 0)]
        [TestCase(2, 10, 10)]
        [TestCase(5, 10, 40)]
        [TestCase(5, 20, 80)]
        public async Task CountAsync_CallRepositoryMethodWithCorrectSkipCount(
            int pageNumber,
            int pageSize,
            int expectedSkipCount)
        {
            const int takeCount = 0;

            await questService.CountAsync(pageNumber, pageSize);

            questRepositoryMock.Verify(
                x => x.CountAsync(
                    It.IsAny<Expression<Func<Quest, bool>>>(),
                    expectedSkipCount,
                    takeCount),
                Times.Once);
        }


        private void SetupMappings()
        {
            mapperMock
                .Setup(x => x.Map<Quest>(questCreateDto))
                .Returns(quest);
        }

        private void SetCurrentUserId()
        {
            currentUserServiceMock
                .Setup(x => x.UserId)
                .Returns(currentUserId);
        }

        private void SetQuestCanBeFound(bool canBeFound)
        {
            var result = canBeFound ? quest : null;

            questRepositoryMock
                .Setup(x => x.GetAsync(quest.Id))
                .ReturnsAsync(result);
        }

        private void SetQuestItemCanBeFound(bool canBeFound)
        {
            var result = canBeFound ? questItem : null;

            questItemRepositoryMock
                .Setup(x => x.GetAsync(questItem.Id))
                .ReturnsAsync(result);
        }

        private void SetUserHasAccessToQuest(bool hasAccess)
        {
            quest.CreatorId = hasAccess ? currentUserId : otherUserId;
        }

        private void SetQuestCompletionNotApproved(bool isNotApproved)
        {
            quest.ApprovedTime = isNotApproved ? null : DateTime.UtcNow;
        }

        private void SetAllQuestItemsHaveFullProgress(bool haveFullProgress)
        {
            foreach (var questItem in quest.QuestItems)
            {
                questItem.Progress = ModelConstants.QuestItem_Progress_MaxValue;
            }

            if (!haveFullProgress)
            {
                quest.QuestItems[0].Progress = ModelConstants.QuestItem_Progress_MaxValue - 1;
            }
        }

        private static Quest CreateQuest()
        {
            return new Quest()
            {
                Id = questId,
                Title = "Test",
                CreatorId = currentUserId,
                DeletedTime = null,
                CreateTime = DateTime.UtcNow - TimeSpan.FromMinutes(10),
                UpdateTime = DateTime.UtcNow - TimeSpan.FromMinutes(5),
                ApprovedTime = null,
                QuestItems = new List<QuestItem>()
                {
                    new QuestItem()
                    {
                        Id = questItemIds[0],
                        QuestId = questId,
                        DeletedTime = null,
                        Progress = 0,
                        Title= "Test item 0",
                    },
                    new QuestItem()
                    {
                        Id = questItemIds[1],
                        QuestId = questId,
                        DeletedTime = null,
                        Progress = 10,
                        Title= "Test item 1",
                    }
                }
            };
        }

        private static QuestItem CreateQuestItem()
        {
            return new()
            {
                Id = questItemIds[0],
                QuestId = questId,
                DeletedTime = null,
                Progress = 0,
                Title = "Test item 0",
            };
        }

        private static QuestCreateDto CreateQuestCreateDto()
        {
            return new()
            {
                Title = "Test",
                QuestItems = new[]
                {
                    new QuestItemCreateDto()
                    {
                        Title = "Test item 0"
                    },
                    new QuestItemCreateDto()
                    {
                        Title = "Test item 1"
                    }
                }
            };
        }

        private static QuestEditDto CreateQuestEditDto()
        {
            return new()
            {
                Id = questId,
                Title = "New Test",
                QuestItems = new[]
                {
                    new QuestItemEditDto()
                    {
                        Id = questItemIds[0],
                        Title = newQuestItemTitles[0]
                    },
                    new QuestItemEditDto()
                    {
                        Id = Guid.Empty,
                        Title = newQuestItemTitles[1]
                    }
                }
            };
        }
    }
}
