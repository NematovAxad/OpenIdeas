using GeneralDomain.EntityModels;
using GeneralDomain.Responses;

namespace GeneralApplication.Interfaces;

public interface IGetByIdGlobalService
{ 
    Response<User> User(int id);
    
    Response<Idea> Idea(int id);

    Response<IdeaComments> Comment(int id);

    Response<Idea> UserIdea(int userId, int ideaId);

    Response<IdeaComments> UserComment(int userId, int commentId);
}