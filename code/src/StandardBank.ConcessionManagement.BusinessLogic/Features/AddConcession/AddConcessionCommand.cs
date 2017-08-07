using MediatR;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession
{
    /// <summary>
    /// Add concession command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{Concession}" />
    public class AddConcessionCommand : IRequest<Concession>
    {
        /// <summary>
        /// Gets or sets the concession.
        /// </summary>
        /// <value>
        /// The concession.
        /// </value>
        public Concession Concession { get; set; }

        /// <summary>
        /// Gets or sets the repository concession
        /// </summary>
        public Model.Repository.Concession RepositoryConcession { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConcessionCommand"/> class.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="user">The user.</param>
        public AddConcessionCommand(Concession concession, User user)
        {
            Concession = concession;
            User = user;
        }
    }
}
