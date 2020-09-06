﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Statiq.Common;
using Statiq.Core;
using Statiq.Web.Pipelines;

namespace Statiq.Web.Modules
{
    public class GetPipelineDocuments : ForAllDocuments
    {
        public GetPipelineDocuments(ContentType contentTypeFilter, params IModule[] dependencyModules)
            : this(Config.FromDocument(doc => doc.Get<ContentType>(WebKeys.ContentType) == contentTypeFilter), dependencyModules)
        {
        }

        /// <param name="inputsFilter">A filter to apply to documents from the <see cref="Inputs"/> pipeline.</param>
        /// <param name="dependencyModules">Modules to run on documents from dependencies.</param>
        public GetPipelineDocuments(Config<bool> inputsFilter, params IModule[] dependencyModules)
            : base(
                new ReplaceDocuments(nameof(Inputs)),
                new FilterDocuments(inputsFilter),
                GetDependencyModule(dependencyModules))
        {
        }

        private static IModule GetDependencyModule(IModule[] dependencyModules)
        {
            ConcatDocuments concatDocuments = new ConcatDocuments
            {
                // Concat (via replace within the concat so it's just these) all documents from externally declared dependencies (exclude explicit dependencies)
                // Add these after the content type filtering since it's assumed a dependency should be added to the pipeline
                new ReplaceDocuments(Config.FromContext<IEnumerable<IDocument>>(ctx => ctx.Outputs.FromPipelines(ctx.Pipeline.GetAllDependencies(ctx).Except(ctx.Pipeline.Dependencies).ToArray()))),

                // Add a media type if one isn't already provided since these might not have been added by ReadFiles
                new ExecuteIf(Config.FromDocument(doc => doc.ContentProvider is object && doc.ContentProvider.MediaType is null && !doc.Source.IsNullOrEmpty))
                {
                    new SetMediaType(Config.FromDocument(doc => doc.Source.MediaType))
                }
            };

            // Add the modules to run on dependencies
            if (dependencyModules is object)
            {
                concatDocuments.Add(dependencyModules);
            }

            return concatDocuments;
        }
    }
}
