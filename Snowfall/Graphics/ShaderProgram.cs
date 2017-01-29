using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Snowfall.Graphics
{
    /*
     * A wrapper for all OpenGL shaders and shader programs
     * Must be disposed before the destructor is invoked
     */
    public class ShaderProgram : IDisposable
    {
        //The most commonly used constructor
        public ShaderProgram(string vertex, string fragment)
        {
            ProgramId = GL.CreateProgram(); //Create shader program handle
            Bind(); //Bind program to context

            int vid = GL.CreateShader(ShaderType.VertexShader); //Create shader object handle
            int fid = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vid, vertex); //Input the shader source code
            GL.ShaderSource(fid, fragment);
            
            GL.CompileShader(vid); //Compile vertex shader
            Snowfall.Log(GL.GetShaderInfoLog(vid)); //Log the shader info log
            int vstatus; GL.GetShader(vid, ShaderParameter.CompileStatus, out vstatus);
            if (vstatus != 1) //Check if shader failed to compile
            {
                Snowfall.Log("Vertex shader compilation failed!"); //Add log entry
                throw new Exception("Vertex shader could not compile!"); //Throw exception
            }

            GL.CompileShader(fid); //Compile fragment shader
            Snowfall.Log(GL.GetShaderInfoLog(vid)); ; //Log the shader info log
            int fstatus; GL.GetShader(fid, ShaderParameter.CompileStatus, out fstatus);
            if (fstatus != 1) //Check if shader failed to compile
            {
                Snowfall.Log("Fragment shader compilation failed!"); //Add log entry
                throw new Exception("Fragment shader could not compile!"); //Throw exception
            }

            GL.AttachShader(ProgramId, vid); //Attach shader object to program
            GL.AttachShader(ProgramId, fid);
            GL.LinkProgram(ProgramId); //Link the program
            GL.DetachShader(ProgramId, vid); //Detach the shader object from program
            GL.DetachShader(ProgramId, fid);
            GL.DeleteShader(vid); //Delete shader objects
            GL.DeleteShader(fid);
        }

        //This constructor is mostly used for the bezier curve
        public ShaderProgram(string vertex, string fragment, string geometry)
        {
            ProgramId = GL.CreateProgram(); //Create shader program handle
            Bind(); //Bind the shader program to context

            int vid = GL.CreateShader(ShaderType.VertexShader); //Create shader object handle
            int fid = GL.CreateShader(ShaderType.FragmentShader);
            int gid = GL.CreateShader(ShaderType.GeometryShader);

            GL.ShaderSource(vid, vertex); //Input the shader source code
            GL.ShaderSource(fid, fragment);
            GL.ShaderSource(gid, geometry);

            GL.CompileShader(vid); //Compile the vertex shader
            Snowfall.Log(GL.GetShaderInfoLog(vid)); //Log the shader info log
            int vstatus; GL.GetShader(vid, ShaderParameter.CompileStatus, out vstatus);
            if (vstatus != 1) //Check if shader failed to compile
            {
                Snowfall.Log("Vertex shader compilation failed!"); //Add log entry
                throw new Exception("Vertex shader could not compile!"); //Throw exception
            }

            GL.CompileShader(fid); //Compile the fragment shader
            Snowfall.Log(GL.GetShaderInfoLog(vid)); //Log the shader info log
            int fstatus; GL.GetShader(fid, ShaderParameter.CompileStatus, out fstatus);
            if (fstatus != 1) //Check if shader failed to compile
            {
                Snowfall.Log("Fragment shader compilation failed!"); //Add log entry
                throw new Exception("Fragment shader could not compile!"); //Throw exception
            }

            GL.CompileShader(gid); //Compile the geometry shadeer
            Snowfall.Log(GL.GetShaderInfoLog(gid)); //Log the shader info log
            int gstatus; GL.GetShader(gid, ShaderParameter.CompileStatus, out gstatus);
            if (gstatus != 1) //Check if shader failed to compile
            {
                Snowfall.Log("Geometry shader compilation failed!"); //Add log entry
                throw new Exception("Geometry shader could not compile!"); //Throw exception
            }

            GL.AttachShader(ProgramId, vid); //Attach shader object to program
            GL.AttachShader(ProgramId, fid);
            GL.AttachShader(ProgramId, gid);

            GL.LinkProgram(ProgramId); //Link the shader program

            GL.DetachShader(ProgramId, vid); //Detach the shader object from the shader program
            GL.DetachShader(ProgramId, fid);
            GL.DeleteShader(vid); //Delete the shader program
            GL.DeleteShader(fid);
        }
        #region Uniform Properties
        public void SetUniform(string name, int value) //Sets the value for a uniform variable
        {
            int index = GL.GetUniformLocation(ProgramId, name); //Get the location of the variable
            //Log and throw an exception if variable doesn't exist
            if (index < 1) { Snowfall.Log("Attempt to use undefined uniform variable!"); throw new Exception("Uniform variable not found!"); } 
            GL.Uniform1(index, value); //Set the uniform value
        }

        public void SetUniform(string name, float value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            if (index < 1) { Snowfall.Log("Attempt to use undefined uniform variable!"); throw new Exception("Uniform variable not found!"); }
            GL.Uniform1(index, value);
        }

        public void SetUniform(string name, double value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            if (index < 1) { Snowfall.Log("Attempt to use undefined uniform variable!"); throw new Exception("Uniform variable not found!"); }
            GL.Uniform1(index, value);
        }

        public void SetUniform(string name, Vector2 value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            if (index < 1) { Snowfall.Log("Attempt to use undefined uniform variable!"); throw new Exception("Uniform variable not found!"); }
            GL.Uniform2(index, value);
        }

        public void SetUniform(string name, Vector3 value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            if (index < 1) { Snowfall.Log("Attempt to use undefined uniform variable!"); throw new Exception("Uniform variable not found!"); }
            GL.Uniform3(index, value);
        }

        public void SetUniform(string name, Vector4 value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            if (index < 1) { Snowfall.Log("Attempt to use undefined uniform variable!"); throw new Exception("Uniform variable not found!"); }
            GL.Uniform4(index, value);
        }

        public void SetUniform(string name, Matrix2 value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            if (index < 1) { Snowfall.Log("Attempt to use undefined uniform variable!"); throw new Exception("Uniform variable not found!"); }
            GL.UniformMatrix2(index, false, ref value);
        }

        public void SetUniform(string name, Matrix3 value)
        {
            int index = GL.GetUniformLocation(ProgramId, name);
            if (index < 1) { Snowfall.Log("Attempt to use undefined uniform variable!"); throw new Exception("Uniform variable not found!"); }
            GL.UniformMatrix3(index, false, ref value);
        }
        #endregion

        //Binds program to context
        public void Bind()
        {
            GL.UseProgram(ProgramId);
        }

        //Safely disposes shader program
        public void Dispose()
        {
            if (ProgramId == -1) //Check if program was already disposed
            {
                Snowfall.Log("Duplicate dispose of shader program!"); //Log the duplicate dispose
                return; //Exit method
            }
            GL.DeleteProgram(ProgramId); //Deletes the program
            ProgramId = -1; //Marks program as disposed
        }

        //Checks if program was safely disposed
        ~ShaderProgram()
        {
            if (ProgramId != -1) //Checks if program was disposed
                Snowfall.Log("Shader program not disposed!"); //Log memory leak
        }

        public string ProgramLog { get { return GL.GetProgramInfoLog(ProgramId); } } //Gets the log of the shader program
        public int ProgramId { get; private set; } //Gets the program handle
    }
}
