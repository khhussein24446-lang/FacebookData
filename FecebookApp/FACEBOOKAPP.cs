using FecebookApp.services;
using FecebookClass;
using System;
using System.Runtime.CompilerServices;
using System.Transactions;
using static FecebookApp.services.PostService;

namespace FecebookApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var context = new FacebookDbContext();

            UserService userService = new UserService(context);
            PostService postServices = new PostService(context);
            CommentService commentService = new CommentService(context);
            FriendService friendService = new FriendService(context);
            GroupService groupService = new GroupService(context);
            NotificationService notificationService = new NotificationService(context);
  
                int? currentUserId = null;

                while (true)
                {
                    Console.Clear();

                    Console.WriteLine("================================");
                    Console.WriteLine("        FACEBOOK PROJECT");
                    Console.WriteLine("================================");
                    Console.WriteLine("1. Register");
                    Console.WriteLine("2. Login");
                    Console.WriteLine("3. Create Post");
                    Console.WriteLine("4. Update Post");
                    Console.WriteLine("5. Delete Post");
                    Console.WriteLine("6. Like Post");
                    Console.WriteLine("7. Comment on Post");
                    Console.WriteLine("8. Reply to Comment");
                    Console.WriteLine("9. Friend Requests");
                    Console.WriteLine("10. Friends");
                    Console.WriteLine("11. Groups");
                    Console.WriteLine("12. Notifications");
                    Console.WriteLine("0. Exit");

                    Console.WriteLine("--------------------------------");
                    Console.Write("Choose: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":

                        Console.WriteLine("enter your username");
                        string name = Console.ReadLine();
                        Console.WriteLine("enter your email");
                        string email = Console.ReadLine();
                        Console.WriteLine("enter your password");
                        string pas = Console.ReadLine();
                        Console.WriteLine("enter  bio");
                        string bio = Console.ReadLine();
                        Console.WriteLine("enter  dateofbirth");
                        DateTime dateofbirth  ;
                        DateTime.TryParse(Console.ReadLine(), out dateofbirth);
                        userService.Register(name,email,pas,bio,dateofbirth);
                            break;

                        case "2":
                        Console.WriteLine("enter your email");
                        string emai = Console.ReadLine();
                        Console.WriteLine("enter your password");
                        string pass = Console.ReadLine();
                        userService.Login(emai, pass);
                            break;

                        case "3":
                        Console.WriteLine("enter your userid");
                        int userid = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your text");
                        string text = Console.ReadLine();
                        postServices.CreatePost(userid, text);
                            break;

                        case "4":
                        Console.WriteLine("enter your userid");
                        int userId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your postid");
                        int postid = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your newtext");
                        string newtext = Console.ReadLine();
                        postServices.UpdatePost(postid,userId, newtext);
                            break;

                        case "5":
                        Console.WriteLine("enter your userid");
                        int uid = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your postid");
                        int postId = int.Parse(Console.ReadLine());
                        postServices.DeletePost(postId, uid);
                            break;
                        case "6":
                        Console.WriteLine("enter your userid");
                        int Userid = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your postid");
                        int PostId = int.Parse(Console.ReadLine());
                        postServices.LikePost(PostId,Userid);
                            break;

                        case "7":
                        Console.WriteLine("enter your userid");
                        int USerId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your postid");
                        int vPostId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your message");
                        string message=Console.ReadLine();
                        commentService.CommentOnPost(vPostId, USerId, message);
                            break;

                        case "8":
                        Console.WriteLine("enter your userid");
                        int USErId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your commentId");
                        int commentId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your message");
                        string Message = Console.ReadLine();
                        commentService.ReplyToComment(commentId, USErId, Message);
                            break;

                        case "9":
                        Console.WriteLine("enter your senderId");
                        int senderId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your receiverId");
                        int receiverId = int.Parse(Console.ReadLine());
                        friendService.SendFriendRequest(senderId, receiverId);
                            break;

                        case "10":
                        Console.WriteLine("enter your friendid");
                        int friendid = int.Parse(Console.ReadLine());
                        friendService.AcceptFriendRequest(friendid);
                            break;

                        case "11":
                        Console.WriteLine("choice from 1:4\n 1. joingroup\n 2.creategroup\n 3.leave \n 4.view");
                        int choic=int.Parse(Console.ReadLine());
                        switch (choic) {
                            case 1:
                                Console.WriteLine("enter your USERId");
                                int USERId = int.Parse(Console.ReadLine());
                                Console.WriteLine("enter your groupId");
                                int groupId = int.Parse(Console.ReadLine());
                                groupService.JoinGroup(USERId, groupId);
                            break;
                                case 2:
                                Console.WriteLine("enter your id");
                                int id = int.Parse(Console.ReadLine());
                                Console.WriteLine("enter groupname");
                                string Name=Console.ReadLine();
                                groupService.CreateGroup(id, Name);
                                break;
                                case 3:
                                Console.WriteLine("enter userid");
                                int USERID = int.Parse(Console.ReadLine());
                                Console.WriteLine("enter userid");
                                int Groupid = int.Parse(Console.ReadLine());
                                groupService.LeaveGroup(USERID, Groupid);
                                break;
                                case 4:
                                groupService.GetAllGroups();
                                break;
                        }
                        break;

                        case "12":
                        NotificationService.ReferenceEquals(notificationService, null);
                            break;

                        case "0":
                            Console.WriteLine("Goodbye!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice!");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }
    }

