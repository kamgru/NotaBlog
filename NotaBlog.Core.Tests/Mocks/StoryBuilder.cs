using NotaBlog.Core.Entities;
using NotaBlog.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Tests.Mocks
{
    class StoryBuilder
    {
        private StoryModel _model;

        public StoryBuilder CreateStory()
        {
            _model = new StoryModel();
            return this;
        }

        public Story Build()
        {
            return new Story(_model.DateTimeProvider);
        }

        public StoryBuilder WithTimeProvider(IDateTimeProvider dateTimeProvider)
        {
            _model.DateTimeProvider = dateTimeProvider;
            return this;
        }

        class StoryModel
        {
            public IDateTimeProvider DateTimeProvider { get; set; }
        }
    }
}
