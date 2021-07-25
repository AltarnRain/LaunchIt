// <copyright file="CommentsObjectGraphVisitor.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Serialization
{
    using YamlDotNet.Core;
    using YamlDotNet.Core.Events;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.ObjectGraphVisitors;

    /// <summary>
    /// Visitor.
    /// Source: https://dotnetfiddle.net/8M6iIE.
    /// </summary>
    public class CommentsObjectGraphVisitor : ChainedObjectGraphVisitor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsObjectGraphVisitor"/> class.
        /// </summary>
        /// <param name="nextVisitor">The next visitor.</param>
        public CommentsObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor)
            : base(nextVisitor)
        {
        }

        /// <summary>
        /// Enters the mapping.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="context">The context.</param>
        /// <returns>A boolean.</returns>
        public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context)
        {
            if (value is CommentsObjectDescriptor commentsDescriptor && commentsDescriptor.Comment != null)
            {
                context.Emit(new Comment(commentsDescriptor.Comment, false));
            }

            return base.EnterMapping(key, value, context);
        }
    }
}