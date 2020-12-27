
using MoviesSubscriptions.Model;
using MoviesSubscriptions.UI.Data.Repositories;
using MoviesSubscriptions.UI.View.Services;
using MoviesSubscriptions.UI.ViewModel.Base;
using MoviesSubscriptions.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.ViewModel
{
    public class MemberDetailsViewModel : DetailViewModelBase
    {
        private readonly IMemberRepository _memberRepository;
        private MemberWrapper _member;

        public MemberDetailsViewModel(IEventAggregator eventAggregator, IMessageDialogService messageDialogService,
            IMemberRepository memberRepository) : base(eventAggregator, messageDialogService)
        {
            _memberRepository = memberRepository;

        }

        public async override Task LoadAsync(int memberId)
        {
            var member = memberId > 0
              ? await EditMember(memberId)
              : CreateMember();

            Id = memberId;

            InitializetMember(member);
        }

        public MemberWrapper Member
        {
            get { return _member; }
            private set
            {
                _member = value;
                OnPropertyChanged();
            }
        }

        private Member CreateMember()
        {
            Member member = new Member();
            _memberRepository.Add(member);
            return member;
        }

        private void InitializetMember(Member member)
        {
            Member = new MemberWrapper(member);
            Member.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _memberRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Member.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if (e.PropertyName == nameof(Member.Name))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Member.Id == 0)
            {
                // Little trick to trigger the validation
                Member.Name = "";
                Member.Email = "";
                Member.City = "";
             }
            SetTitle();
        }

        private void SetTitle()
        {
            Title = $"{Member.Name}";
        }

        private async Task<Member> EditMember(int memberId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);
            return member;
        }


        protected async override void OnDeleteExecute()
        {
            var result = await MessageDialogService.ShowOkCancelDialogAsync($"Do you really want to delete the member {Member.Name}?", "Question");
            if (result == MessageDialogResult.OK)
            {
                _memberRepository.Remove(Member.Model);
                await _memberRepository.SaveAsync();
                RaiseDetailDeletedEvent(Member.Id);
            }
        }


        protected override bool OnSaveCanExecute()
        {
            return Member != null && !Member.HasErrors && HasChanges;
        }

        protected async override void OnSaveExecute()
        {
            await _memberRepository.SaveAsync();
            HasChanges = _memberRepository.HasChanges();
            Id = Member.Id;
            RaiseDetailSavedEvent(Member.Id);
        }


    }
}
